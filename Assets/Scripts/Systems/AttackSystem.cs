using UnityEngine;

public class AttackSystem : MonoBehaviour
{

    public bool attackAble = true;
    //determines if the character can attack at all depending on if they are on cooldown
    public bool dealDamage = false;
    //returns true if opponents are in range for damage from attacks to take place
    //false if players are too far apart, depending on the range attribute of technique
    public bool perform = false;
    //determines whether the sprite will change to show an attack has been performed
    //moreso acts as a debugging tool since the state of perform can be checked in unity when paused

    private float lastAttackTime = 0f;
    private float lastJumpTime = 0f;
    private float performTime = 1.0f;
    //keep track of when moves have been performed like an attack/jump/sprite change
    //resets the character's cooldown or sprite this amount of time

    public SpriteRenderer spriteRenderer;
    public Sprite kickSprite;
    public Sprite punchSprite;
    public Sprite standingSprite;
    //to be linked in unity to the character of the sprite and the picture for each sprite

    public Technique frontKick = new Technique(10, 10, 0.25f, 2.5f, 150);
    public Technique crescentKick = new Technique(20, 25, 0.75f, 2f, 250);
    public Technique jab = new Technique(5, 5, 0.25f, 1.5f, 100);
    public Technique uppercut = new Technique(15, 20, 0.5f, 1.5f, 150);
    //the 4 attacks all instantiated
    [System.Serializable]
    public class Technique
    {
        public int damage;
        public int stagger;
        public float cooldown;
        public float range;
        public int points;
        //attributes of technique

        public Technique(int Damage, int Stagger, float Cooldown, float Range, int Points)
        //constructor method
        {
            damage = Damage;
            stagger = Stagger;
            cooldown = Cooldown;
            range = Range;
            points = Points;
        }
    }

    public float DistanceBetween(Transform self, Transform target)
    //calculates the distance between 2 game objects, self and target character
    {
        return Vector2.Distance(self.position, target.position);
    }

    private bool InRange(Technique technique, Transform self, Transform target)
    //return a bool value depending on the distance between players and the maximum range of the attack
    {
        float distance = DistanceBetween(self, target);
        if (distance <= technique.range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StopCooldown()
    {
            attackAble = true;
    }
    
    private void StopPerform()
    {
        perform = false;
        spriteRenderer.sprite = standingSprite;
        //resets sprite to default
    }

    public void Attack(Technique technique, Transform self, Transform target)
    {
        if (attackAble == false)
        {
            Debug.Log("On cooldown");
        }

        else
        {
            HealthSystem targetHealthSystem = target.GetComponent<HealthSystem>();
            //obtains the health system of other character

            if (InRange(technique, self, target) == true)
            {
                dealDamage = true;
                Debug.Log("Successful attack by " + self.name);
                //writing to console for debugging
                targetHealthSystem.DealEffects(technique);
                //calls for effects to be dealt using the other player's health system
            }

            else
            {
                dealDamage = false;
                Debug.Log("Failed attack!");
                //out of attack's range
            }

            lastAttackTime = Time.time;

            attackAble = false;
            //cooldown initiates
            Invoke("StopCooldown", technique.cooldown);
            //calls for cooldown to end after a certain time has passed

            perform = true;
            ChangeSprite(technique);
            //changes sprite of character

            Invoke("StopPerform", performTime);
            //calls for the sprite to change back after a certain amount of time
        }
    }

    public void Move(string direction)
    {
        Vector2 pos = transform.position;
        //obtains current position
        float moveSpeed = 0.1f;
        //units moved per frame

        if (direction == "left" && pos.x > -4)
        {
            pos.x -= moveSpeed;
        }
        if (direction == "right" && pos.x < 4)
        {
            pos.x += moveSpeed;
        }
        if (direction == "up" && pos.y < 0.75 && (lastJumpTime + 0.8f) < Time.time)
        {
            pos.y += 1.2f;
            lastJumpTime = Time.time;
            //cooldown on jumping
            //avoid jumping too high up
        }
        //using conditional logical and operator since it short circuits if a condition surely wont be met

        transform.position = pos;
        //updates to new position
    }

    public void ChangeSprite(Technique technique)
    {
        if (perform == true)
        {
            if (technique == frontKick || technique == crescentKick)
            //kicks given same sprite
            {
                spriteRenderer.sprite = kickSprite;
            }
            else if (technique == jab || technique == uppercut)
            //punches given same sprite
            {
                spriteRenderer.sprite = punchSprite;
            }
            //using conditional logical or operator to short circuit
            //less error prone
        }
        else
        {
            spriteRenderer.sprite = standingSprite;
            //default to standing sprite
        }
    }
}
