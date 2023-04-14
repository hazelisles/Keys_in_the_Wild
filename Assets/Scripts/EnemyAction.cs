using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    private bool isAlive = true;
    private int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
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
                health--;
                anim.SetTrigger("gethit");
            }
            if (anim != null && health <= 0)
            {
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
