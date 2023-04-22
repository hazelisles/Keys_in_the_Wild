using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private float floatTimer;
    private float floatSpeed = 0.0045f;
    private float floatRate = 1f;

    [SerializeField] private GameObject enemyPrefab;
    int enemyCount = 0;
    private List<EnemyNPC> enemies = new List<EnemyNPC>();


    private void Start()
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
        if (Time.timeScale > 0)
        {
            // key rotate itself
            Vector3 rotation = Vector3.up * 150 * Time.deltaTime;
            transform.Rotate(rotation, Space.World);
            // key float up and down
            floatTimer += Time.deltaTime;
            Vector3 movement = new Vector3(0f, floatSpeed, 0f);
            transform.Translate(movement);

            if (floatTimer >= floatRate)
            {
                floatTimer = 0;
                floatSpeed = -floatSpeed;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Messenger.Broadcast(GameEvent.COLLECT_KEY);
            Destroy(this.gameObject);
        }
    }
}
