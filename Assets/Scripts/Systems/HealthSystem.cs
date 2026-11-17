using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int currentHealth = 100;
    public int currentStagger = 100;
    //this can be modified in the future for whatever reason
    //e.g. for making rounds longer

    public bool staggered = false;
    public bool blocking = false;
    //damage received affecting states for the characters to be in

    public int damageDealt = 0;
    public float startStaggeredTime = 0f;
    //cooldown logic for how long a character is staggered for

    public AttackSystem attackSystem;

    public void DealEffects(AttackSystem.Technique technique)
    {
        if (staggered == true)
        {
            damageDealt = Mathf.RoundToInt(technique.damage * 1.5f);
            //increased damage when staggered
        }

         else if (blocking == true)
        {
            damageDealt = Mathf.RoundToInt(technique.damage * 0.8f);
            //decreased damage when blocking
        }

        //rounds the values to integers to avoid complex and hard to read point calculations

        else
        {
            damageDealt = technique.damage;
            //not in a special state
        }

        currentHealth -= damageDealt;
        //receive health point damage
        Debug.Log(gameObject.name + " received " + damageDealt + " damage. Current health: " + currentHealth);
        //written in console for debugging

        if ((currentStagger -= technique.stagger) < 0)
        {
            currentStagger = 0;
            staggered = true;
            startStaggeredTime = Time.time;
            Debug.Log("Staggered!");
            //avoids displaying a negative stagger
        }
        else
        {
            currentStagger -= technique.stagger;
        }
        
        Debug.Log(gameObject.name + " received " + technique.stagger + " stagger. Current stagger: " + currentStagger);
    }

}
