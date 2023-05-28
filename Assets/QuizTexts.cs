using UnityEngine;

[System.Serializable]
public class Question
{
    [TextArea(3,10)]
    public string question;
    [TextArea(2,10)]
    public string[] answers;
    public int correct;
    public Sprite background;
}
