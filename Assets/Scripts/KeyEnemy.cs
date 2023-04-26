using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEnemy : MonoBehaviour
{
    // array for enemies prefab
    public GameObject[] enemyPrefabs = new GameObject[2];
    // projectile prefab
    public GameObject projectile;
    // projectile explosion effect prefab
    public GameObject impactPrefab;
    int enemyCount = 0;
    private List<EnemyNPC> enemies = new List<EnemyNPC>();
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = Random.Range(1, 4);
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyObj = Instantiate(enemyPrefabs[Random.Range(0,2)]) as GameObject;
            EnemyNPC enemy = enemyObj.GetComponent<EnemyNPC>();


            enemy.SetPrizeLoc(this.transform);
            enemy.SetProjectilePrefab(projectile, impactPrefab);
            enemy.DetermineNextWaypoint();

            //Debug.Log("keypos:" + transform.position);
            enemyObj.transform.position = enemy.GetCurrentWaypoint();
            //Debug.Log("current waypoint:" + enemy.GetCurrentWaypoint());


            enemyObj.transform.rotation = Quaternion.identity;
            enemies.Add(enemy);
            EnemyManager.Instance.addEnemy(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
