using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public void game()
    {
        SceneManager.LoadScene(1);
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
    }
}
