using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager Instance { get; private set; } = null;

    // list of all enemies
    private List<EnemyNPC> enemyNPCs = new List<EnemyNPC>();

    private int enemyChaseCount = 0;
    private bool soundon = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addEnemy(EnemyNPC enemy)
    {
        enemyNPCs.Add(enemy);
    }

    public void destroyEnemy(EnemyNPC enemy) 
    {
        enemyNPCs.Remove(enemy);
    }

    public void battleSound()
    {
        enemyChaseCount = 0;
        foreach (EnemyNPC enemy in enemyNPCs)
        {
            if (enemy.isChasing) { enemyChaseCount++; }
        }
        if(enemyChaseCount == 1 && !soundon)
        {
            Debug.Log("Sound On");
            Messenger.Broadcast(GameEvent.BATTLE_SOUND_ON);
            soundon = true;
        }
        else if(enemyChaseCount == 0 && soundon)
        {
            Debug.Log("Sound Off");
            Messenger.Broadcast(GameEvent.BATTLE_SOUND_OFF);
            soundon = false;
        }
    }
}
