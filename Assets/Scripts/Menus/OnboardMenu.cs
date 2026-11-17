using UnityEngine;
using UnityEngine.SceneManagement;

public class OnboardMenu : MonoBehaviour
{
    public void BeginGame()  //direct player to gameplay screen
    {
        SceneManager.LoadScene("game screen");
    }
}
