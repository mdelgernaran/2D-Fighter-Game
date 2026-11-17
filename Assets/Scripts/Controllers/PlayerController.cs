using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    public Transform AI;
    //to obtain the position of the ai & player using unity's built in function
    public AttackSystem attackSystem;
    public HealthSystem healthSystem;
    //removed health system after moving block logic to attack system
    //importing attack system only for move()

    private void Update()
    //runs function to read the input every frame
    {
        ReadInput();
    }

    private void ReadInput()
    {
        //using standard controls for ease in transferring to this game from others

        if (Input.GetKey(KeyCode.A))
        {
            attackSystem.Move("left");
        }

        else if (Input.GetKey(KeyCode.D))
        {
            attackSystem.Move("right");
        }

        else if (Input.GetKey(KeyCode.Space))
        {
            attackSystem.Move("up");
        }

        else if (Input.GetKey(KeyCode.E))
        {
            attackSystem.Attack(attackSystem.frontKick, player, AI);
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            attackSystem.Attack(attackSystem.crescentKick, player, AI);
        }

        else if (Input.GetMouseButtonDown(0))
        {
            attackSystem.Attack(attackSystem.jab, player, AI);
        } 

        else if (Input.GetMouseButtonDown(1))
        {
            attackSystem.Attack(attackSystem.uppercut, player, AI);
        }

        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        //either shift works, accommodates for those who are left handed or differently abled
        {
            healthSystem.blocking = true;
        }

        else
        {
            healthSystem.blocking = false;
            //ensures blocking isn't always on
        }
    }
}
