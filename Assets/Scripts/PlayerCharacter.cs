using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int keyCount = 0;

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
        keyCount++;
        Debug.Log(keyCount);
    }
}
