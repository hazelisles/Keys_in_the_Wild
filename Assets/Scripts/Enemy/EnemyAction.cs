using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    private bool isAlive = true;
    private int hitDamage = 2;
    private int turtleScore = 2;
    private int slimeScore = 1;
    private int health;
    private int maxHealth;
    private EnemyUIC enemyUI;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "turtle") { 
            health = 10;
            maxHealth = 10;
        }
        if (gameObject.tag == "slime") { 
            health = 5;
            maxHealth = 5;
        }
        enemyUI = gameObject.GetComponentInChildren<EnemyUIC>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReactToHit()
    {
        Animator anim = GetComponent<Animator>();

        if (isAlive)
        {
            if (anim != null && health > 0)
            {
                health -= hitDamage;
                enemyUI.UpdateHealth((float)health/maxHealth);
                anim.SetTrigger("gethit");
            }
            if (anim != null && health <= 0)
            {
                if(gameObject.tag == "turtle")
                {
                    Messenger<int>.Broadcast(GameEvent.ENEMY_DEAD, turtleScore);
                }
                if(gameObject.tag == "slime")
                {
                    Messenger<int>.Broadcast(GameEvent.ENEMY_DEAD, slimeScore);
                }
                anim.SetTrigger("Die");
                isAlive = false;                
            }

        }
    }

    // Animator deadevent takeover coroutine Die()
    private void DeadEvent()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bat")
        {
            //Debug.Log("Get hit!");
            ReactToHit();
            //Debug.Log(gameObject.name + " health: " + health.ToString());
        }
    }
}
