using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("onboard screen");
    }

    public void QuitGame()
    {
        Debug.Log("Application quit"); //displays to console when quit button is selected even in editor
        Application.Quit();
    }
}
