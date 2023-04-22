using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
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
    //private Transform originalpos;

    public List<Transform> Waypoints { get; private set; }      // waypoints for patrol state
    private int waypointIndex = 0;                              // current waypoint index

    //[SerializeField] private GameObject projectilePrefab;       // for creating "bullets"
    //[SerializeField] public Transform projectileSpawnPt;        // spawn point for bullets    
    //private float projectileForce = 35f;                        // force to shoot the projectile with


    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        //originalpos = this.gameObject.transform;

        // Create and populate a list of waypoints
        Waypoints = new List<Transform>();
        //Waypoints.Add(originalpos);
        GameObject waypoints = transform.Find("Waypoints").gameObject;
        foreach(Transform t in waypoints.transform)
        {
            Waypoints.Add(t);
        }
        
    }

    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }

    public void DetermineNextWaypoint()
    {
        //pick a random waypoint
        waypointIndex = Random.Range(0, Waypoints.Count);
    }

    public Vector3 GetCurrentWaypoint()
    {
        //return the current waypoint
        return Waypoints[waypointIndex].position;
    }


}
