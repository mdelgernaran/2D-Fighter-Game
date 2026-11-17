using UnityEngine;
using System;

public class AIController : MonoBehaviour
{
    private System.Random rnd = new System.Random();
    //instantiate a random number from the random class
    //in the range of 0.0 and 1.0
    public AttackSystem attackSystem;
    public HealthSystem healthSystem;

    public Transform player;
    public Transform AI;

    private void Update()
    {
        ChangeState();
    }

    private void ChangeState()
    //direct implementation of my decision tree
    //none of these inequalities are mandatory and can be adjusted as neededed
    {
        float distance = attackSystem.DistanceBetween(AI, player);
        bool attackAble = attackSystem.attackAble;
        int health = healthSystem.currentHealth;

        //declare all these variables for simpler use and readability

        if (attackAble == false)
        {
            Evade();
        }

        else if (healthSystem.currentHealth < 20)
        {
            if (distance < 2)
            //consciously attacking despite not always being close enough
            {
                if (rnd.NextDouble() > 0.25)
                {
                    Evade();
                    //prefer evading
                }

                //5% chance of each attack
                else if (rnd.NextDouble() < 0.05)
                {
                    attackSystem.Attack(attackSystem.frontKick, AI, player);
                }

                else if (rnd.NextDouble() < 0.10)
                {
                    attackSystem.Attack(attackSystem.crescentKick, AI, player);
                }

                else if (rnd.NextDouble() < 0.15)
                {
                    attackSystem.Attack(attackSystem.jab, AI, player);
                }

                else if (rnd.NextDouble() < 0.25)
                {
                    attackSystem.Attack(attackSystem.uppercut, AI, player);
                }
            }

            else
            {
                healthSystem.blocking = true;
            }
        }

        else if (distance <= 1)
        {
            //randomly choosing a heavier attack
            //33% chance of it being uppercut
            //67% chance of it being a crescent kick
            if (rnd.NextDouble() > 0.33)
            {
                attackSystem.Attack(attackSystem.crescentKick, AI, player);
            }
            else
            {
                attackSystem.Attack(attackSystem.uppercut, AI, player);
            }            
        }

        else
        //far from player but high health and not on cooldown
        {
            Confront();
        }
    }

    private void Evade()
    //moving away from player
    //decided on the relation of the player to the ai
    {
        if (AI.position.y - player.position.y >= 0)
        {
            attackSystem.Move("up");
        }

        if (AI.position.x - player.position.x > 0)
        {
            attackSystem.Move("right");
        }
        else
        {
            attackSystem.Move("left");
        }
    }

    private void Confront()
    //moving towards the player
    {
        if (AI.position.y - player.position.y < 0)
        {
            attackSystem.Move("up");
        }

        if (AI.position.x - player.position.x > 0)
        {
            attackSystem.Move("left");
        }
        else
        {
            attackSystem.Move("right");
        }
    }
}