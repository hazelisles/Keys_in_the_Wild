using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private UIController ui;
    private int keyCount = 0;
    private int health = 100;
    [SerializeField] private AudioClip collect;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.COLLECT_KEY, OnCollectKey);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.COLLECT_KEY, OnCollectKey);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollectKey()
    {
        SoundManager.Instance.PlaySfx(collect);
        keyCount++;
        ui.UpdateKeyCount(keyCount);
        if (keyCount == 5)
        {
            Messenger.Broadcast(GameEvent.GAME_OVER);
        }
        //Debug.Log(keyCount);
    }

    private void ReactToHit(int damagePoint)
    {
        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null && health > 0)
        {
            anim.SetTrigger("gethit");
            health -= damagePoint;
            ui.SetPlayerHealth(health);
        }
        if (anim != null && health <= 0)
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
