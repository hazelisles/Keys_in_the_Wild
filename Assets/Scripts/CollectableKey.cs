using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private float floatTimer;
    private float floatSpeed = 0.0045f;
    private float floatRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Messenger.Broadcast(GameEvent.COLLECT_KEY);
            Destroy(this.gameObject);
        }
    }
}
