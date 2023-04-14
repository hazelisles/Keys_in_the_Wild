using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    [SerializeField] private CapsuleCollider batCollider;   // Get baseball bat capsule collider
    //[SerializeField] private GameObject baseballBat;        // Get baseball bat gameobject
    // Start is called before the first frame update
    void Start()
    {
        // Set baseball bat collider disabled in the beginning
        batCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Control baseball bat collider enable
    public void AttackStart()
    {
        batCollider.enabled = true;
    }
    // Control baseball bat collider disable
    public void AttackEnd()
    {
        batCollider.enabled = false;
    }
}
