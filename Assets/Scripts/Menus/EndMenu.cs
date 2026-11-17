using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{
    public TMP_Text winnerString;
    //displays the winner on the actual screen
    
    public void Awake()
    //called upon the start of the program
    {
        GameOverDisplay();
    }

    public void GameOverDisplay()  //display the winner and score
    {
        winnerString.text = "WINNER: " + MatchManager.winner;  //reads the winner of the game from the MatchManager script
    }
    
    public void PlayAgain()  //regress one scene
    {
        SceneManager.LoadScene("game screen");
    }

    public void ReturnStart()  //return to the start menu
    {
        SceneManager.LoadScene("start screen");
    }

    public void Quit()  //exit the application
    {
        Debug.Log("Application quit");
        Application.Quit();
    }
}