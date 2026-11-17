using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MatchManager : MonoBehaviour
{
    public Transform player;
    public Transform AI;

    private Vector2 initialPlayerPos = new Vector2 (-3, -1.4f);
    private Vector2 initialAIPos = new Vector2 (3, -1.4f);

    private int roundCount = 0;
    private int AIWins = 0;
    private int playerWins = 0;
    private const int maxHealth = 100;
    private const int maxStagger = 100;

    private float staggeredEffectTime = 3f;
    //how long a character is staggered for

    public static string winner = "";
    public TMP_Text playerHealthText;
    public TMP_Text playerStaggerText;
    public TMP_Text aiHealthText;
    public TMP_Text aiStaggerText;
    public TMP_Text roundAnnounceText;

    public HealthSystem playerHealthSystem;
    public HealthSystem aiHealthSystem;

    private void Update()
    {
        GameWon();
        HUD();
        CheckStaggered();
    }
    private void CheckStaggered()
    //updates effective staggered state timer of both players
    //first branch: see who's staggered
    //second branch: see if the player should no longer be staggered
    {
        if (playerHealthSystem.staggered == true)
        {
            if ((playerHealthSystem.startStaggeredTime + staggeredEffectTime) >= Time.time)
            {
                playerHealthSystem.staggered = false;
                playerHealthSystem.currentStagger = maxStagger;
                //resets
                Debug.Log("Player is no longer staggered");
            }
        }
        else if (aiHealthSystem.staggered == true)
        {
            if ((aiHealthSystem.startStaggeredTime + staggeredEffectTime) >= Time.time)
            {
                aiHealthSystem.staggered = false;
                aiHealthSystem.currentStagger = maxStagger;
                //resets
                Debug.Log("AI is no longer staggered");
            }
        }
    }
    public void HUD()
    {
        playerHealthText.text = "HEALTH: " + playerHealthSystem.currentHealth;
        playerStaggerText.text = "STAGGER: " + playerHealthSystem.currentStagger;
        aiHealthText.text = "HEALTH: " + aiHealthSystem.currentHealth;
        aiStaggerText.text = "STAGGER: " + aiHealthSystem.currentStagger;
        roundAnnounceText.text = "ROUND: " + (roundCount + 1);
        //displays the correct current round at all times
    } 
    private void RoundEnd()
    {
        Debug.Log("ROUND OVER");
        roundCount += 1;
        if (playerWins < 2 && AIWins < 2)
        {
            Debug.Log("COMMENCING ROUND: " + (roundCount + 1));
        }
        //avoids displaying commencing round if there is no subsequent round needed
    }
    private void GameEnd()
    {
        Debug.Log("GAME OVER");
    }

    private void RoundRestart()
    {
        playerHealthSystem.currentHealth = 100;
        playerHealthSystem.currentStagger = 100;

        aiHealthSystem.currentHealth = 100;
        aiHealthSystem.currentStagger = 100;

        player.position = initialPlayerPos;
        AI.position = initialAIPos;
    }

    private void GameRestart()
    {
        RoundRestart();
        roundCount = 0;
        playerWins = 0;
        AIWins = 0;
    }

    private void GameWon()
    {
        if (playerHealthSystem.currentHealth <= 0 || aiHealthSystem.currentHealth <= 0)
        {
            if (playerHealthSystem.currentHealth <= 0)
            {
                Debug.Log("CPU wins round " + (roundCount + 1));
                AIWins += 1;
            }
            else
            {
                Debug.Log("Player wins round " + (roundCount + 1));
                playerWins += 1;
            }
            RoundEnd();

            if (roundCount < 3 && playerWins < 2 && AIWins < 2)
            {
                RoundRestart();
                Debug.Log("Begin round " + (roundCount + 1));
            }
            else
            {
                if (playerWins > AIWins)
                {
                    winner = "Player";
                }
                else
                {
                    winner = "AI";
                }
                SceneManager.LoadScene("end screen");
            }
        }
    }
}
