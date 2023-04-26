using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void ReactToHit(int damagePoint)
    {
        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null && gm.playerhealth > 0)
        {
            anim.SetTrigger("gethit");
            Messenger<int>.Broadcast("PLAYER_HEALTH_CHANGE", damagePoint);
        }
        if (anim != null && gm.playerhealth <= 0)
        {
            anim.SetTrigger("die");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "slime")
        {
            ReactToHit(1);
        }
        if(other.tag == "turtle")
        {
            ReactToHit(3);
        }
    }

    private void DeadEvent()
    {
        Messenger.Broadcast(GameEvent.GAME_OVER);
    }
}
