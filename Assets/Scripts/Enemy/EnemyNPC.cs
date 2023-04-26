//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNPC : MonoBehaviour
{
    public float IdleTime { get; private set; } = 1.5f;         // time to spend in idle state
    public float ChaseRange { get; private set; } = 15.0f;      // when player is closer than this, chase
    public float AttackRange { get; private set; } = 4.0f;      // when player is closer than this, attack
    public float AttackRangeStop { get; private set; } = 8.0f; // when player is farther than this, chase

    public GameObject Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    [SerializeField] private GameObject projectilePrefab;       // for creating "bullets"
    private Transform projectileSpawnPt;        // spawn point for bullets    
    private float projectileForce = 2.2f;                        // force to shoot the projectile with

    [SerializeField] private Transform prizeLoc;    // centre reference place to auto generate next waypoint
    public float PrizeRadius { get; private set; } = 7;     // range radius from centre for random next waypoint 
    private Vector3 destination;

    public bool isChasing { get; private set; } = false;


    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        projectileSpawnPt = gameObject.transform.Find("spawnPoint");
    }

    private void Update()
    {
        if(GetDistanceFromPlayer() <= ChaseRange)
        {
            isChasing = true;
        } 
        else if (GetDistanceFromPlayer() > ChaseRange)
        {
            isChasing = false;
        }
    }

    public void SetPrizeLoc(Transform t)
    {
        prizeLoc = t;
        //Debug.Log("prize loc:" + prizeLoc.position);
    }

    public void SetProjectilePrefab(GameObject projectile, GameObject impact)
    {
        Projectile pro = projectile.GetComponent<Projectile>();
        pro.SetImpactPrefab(impact);
        projectilePrefab = projectile;
    }

    Vector3 GetRandomWaypoint()
    {
        Vector2 offset = Random.insideUnitCircle.normalized * PrizeRadius;
        Vector3 waypoint = prizeLoc.position + new Vector3(offset.x, 0, offset.y);
        return waypoint;
    }

    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }

    public void DetermineNextWaypoint()
    {
        destination = GetRandomWaypoint();
        //pick a random waypoint
        //waypointIndex = Random.Range(0, Waypoints.Count);
    }

    public Vector3 GetCurrentWaypoint()
    {
        return destination;
    //    //return the current waypoint
    //    //return Waypoints[waypointIndex].position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(destination, 0.25f);
    }

    public void ShootEvent()
    {
        // spawn a projectile using the spawnPoint
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPt.position, projectileSpawnPt.rotation);
        // move it forward
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
    }

}
