using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject impactPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject impact = Instantiate(impactPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 2);
        Destroy(gameObject);
    }

    public void SetImpactPrefab(GameObject impact)
    {
        impactPrefab = impact;
    }
}
