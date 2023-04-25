using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    int enemyCount = 0;
    private List<EnemyNPC> enemies = new List<EnemyNPC>();
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = Random.Range(1, 3);
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyObj = Instantiate(enemyPrefab) as GameObject;
            EnemyNPC enemy = enemyObj.GetComponent<EnemyNPC>();


            enemy.SetPrizeLoc(this.transform);
            enemy.DetermineNextWaypoint();

            //Debug.Log("keypos:" + transform.position);
            enemyObj.transform.position = enemy.GetCurrentWaypoint();
            //Debug.Log("current waypoint:" + enemy.GetCurrentWaypoint());


            enemyObj.transform.rotation = Quaternion.identity;
            enemies.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
