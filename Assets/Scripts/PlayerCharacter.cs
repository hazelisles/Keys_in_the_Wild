using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private AudioClip ajOugh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void ReactToHit(int damagePoint)
    {
        Animator anim = GetComponent<Animator>();
        SoundManager.Instance.PlaySfx(ajOugh);
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

    private void DeadEvent()
    {
        Messenger.Broadcast(GameEvent.GAME_OVER);
    }
}
