using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizTrigger : MonoBehaviour
{
    public Question[] questions;

    [SerializeField] TMPro.TMP_Text question, answer1, answer2, answer3, answer4, menu;
    [SerializeField] Image[] bg;
    [SerializeField] int questionIndex = -1;
    [SerializeField] AudioSource sounds;
    [SerializeField] AudioClip correct;
    [SerializeField] AudioClip incorrect;
    [SerializeField] bool firstImage = true;
    int score = 0;
    private void Start()
    {
        StartCoroutine(NextQuestion(0));
        bg[0].color = new Color(1, 1, 1, 1);
        bg[1].color = new Color(0, 0, 0, 0);
        question.text = questions[questionIndex].question;
        answer1.text = questions[questionIndex].answers[0];
        answer2.text = questions[questionIndex].answers[1];
        answer3.text = questions[questionIndex].answers[2];
        answer4.text = questions[questionIndex].answers[3];
        menu.color = new Color(0, 0, 0, 0);
        menu.transform.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        menu.transform.parent.gameObject.SetActive(false);
    }

    IEnumerator NextQuestion(int wait)
    {
        questionIndex++;
        if(wait != 0 && questionIndex < questions.Length)
        {
            bg[firstImage ? 1 : 0].sprite = questions[questionIndex].background;
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                if (bg[firstImage ? 1 : 0].color.a < 1)
                    bg[firstImage ? 1 : 0].color += new Color(1, 1, 1, 0.01f);

                if (bg[firstImage ? 0 : 1].color.a > 0)
                    bg[firstImage ? 0 : 1].color -= new Color(0, 0, 0, 0.01f);

                if (bg[firstImage ? 1 : 0].color.a > .5f)
                {
                    answer1.color += new Color(0, 0, 0, 0.02f);
                    answer2.color += new Color(0, 0, 0, 0.02f);
                    answer3.color += new Color(0, 0, 0, 0.02f);
                    answer4.color += new Color(0, 0, 0, 0.02f);
                    question.color += new Color(0, 0, 0, 0.02f);
                    answer1.transform.parent.GetComponent<Image>().color += new Color(1, 1, 1, 0.02f);
                    answer2.transform.parent.GetComponent<Image>().color += new Color(1, 1, 1, 0.02f);
                    answer3.transform.parent.GetComponent<Image>().color += new Color(1, 1, 1, 0.02f);
                    answer4.transform.parent.GetComponent<Image>().color += new Color(1, 1, 1, 0.02f);
                }
                if (bg[firstImage ? 1 : 0].color.a < .5f)
                {
                    answer1.color -= new Color(0, 0, 0, 0.02f);
                    answer2.color -= new Color(0, 0, 0, 0.02f);
                    answer3.color -= new Color(0, 0, 0, 0.02f);
                    answer4.color -= new Color(0, 0, 0, 0.02f);
                    question.color -= new Color(0, 0, 0, 0.02f);
                    answer1.transform.parent.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
                    answer2.transform.parent.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
                    answer3.transform.parent.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
                    answer4.transform.parent.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
                }


                if (Mathf.Approximately(bg[firstImage ? 1 : 0].color.a, .5f))
                {
                    question.text = questions[questionIndex].question;
                    answer1.text = questions[questionIndex].answers[0];
                    answer2.text = questions[questionIndex].answers[1];
                    answer3.text = questions[questionIndex].answers[2];
                    answer4.text = questions[questionIndex].answers[3];
                }
            }
            firstImage = !firstImage;
        }
        else if (questionIndex >= questions.Length)
        {
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                if (answer1.color.a > 0)
                {
                    answer1.color -= new Color(0, 0, 0, 0.02f);
                    answer2.color -= new Color(0, 0, 0, 0.02f);
                    answer3.color -= new Color(0, 0, 0, 0.02f);
                    answer4.color -= new Color(0, 0, 0, 0.02f);
                    question.color -= new Color(0, 0, 0, 0.02f);
                    answer1.transform.parent.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
                    answer2.transform.parent.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
                    answer3.transform.parent.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
                    answer4.transform.parent.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
                }
                else
                {
                    question.color += new Color(0, 0, 0, 0.02f);
                    menu.transform.parent.gameObject.SetActive(true);
                    menu.color += new Color(0, 0, 0, 0.02f);
                    menu.transform.parent.GetComponent<Image>().color += new Color(0, 0, 0, 0.02f);
                    question.text = "Очки:" + score + "/" + questions.Length;
                }
            }
            answer1.transform.parent.gameObject.SetActive(false);
            answer2.transform.parent.gameObject.SetActive(false);
            answer3.transform.parent.gameObject.SetActive(false);
            answer4.transform.parent.gameObject.SetActive(false);
        }

        answer1.transform.parent.GetComponent<Button>().enabled = true;
        answer2.transform.parent.GetComponent<Button>().enabled = true;
        answer3.transform.parent.GetComponent<Button>().enabled = true;
        answer4.transform.parent.GetComponent<Button>().enabled = true;
    }
    public void Answer(int index)
    {
        if (questionIndex < questions.Length)
        {
            if (questions[questionIndex].correct == index)
            {
                score ++;
                sounds.clip = correct;
                sounds.Play();
            }
            else
            {
                sounds.clip = incorrect;
                sounds.Play();
            }
            if (questions[questionIndex].correct == 1)
            {
                answer1.transform.parent.GetComponent<Image>().color = Color.green;
                answer2.transform.parent.GetComponent<Image>().color = Color.red;
                answer3.transform.parent.GetComponent<Image>().color = Color.red;
                answer4.transform.parent.GetComponent<Image>().color = Color.red;
            }
            if (questions[questionIndex].correct == 2)
            {
                answer1.transform.parent.GetComponent<Image>().color = Color.red;
                answer2.transform.parent.GetComponent<Image>().color = Color.green;
                answer3.transform.parent.GetComponent<Image>().color = Color.red;
                answer4.transform.parent.GetComponent<Image>().color = Color.red;
            }
            if (questions[questionIndex].correct == 3)
            {
                answer1.transform.parent.GetComponent<Image>().color = Color.red;
                answer2.transform.parent.GetComponent<Image>().color = Color.red;
                answer3.transform.parent.GetComponent<Image>().color = Color.green;
                answer4.transform.parent.GetComponent<Image>().color = Color.red;
            }
            if (questions[questionIndex].correct == 4)
            {
                answer1.transform.parent.GetComponent<Image>().color = Color.red;
                answer2.transform.parent.GetComponent<Image>().color = Color.red;
                answer3.transform.parent.GetComponent<Image>().color = Color.red;
                answer4.transform.parent.GetComponent<Image>().color = Color.green;
            }
        }
        answer1.transform.parent.GetComponent<Button>().enabled = false;
        answer2.transform.parent.GetComponent<Button>().enabled = false;
        answer3.transform.parent.GetComponent<Button>().enabled = false;
        answer4.transform.parent.GetComponent<Button>().enabled = false;
        StartCoroutine(NextQuestion(1));
    }

}
