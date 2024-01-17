using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDegenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private new Collider collider;
    void Start()
    {
        gameObject.TryGetComponent<Collider>(out collider);
        if (collider == null)
        {
            Debug.LogError("collider is not found.");
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");

        // 6 is Enemy layer
        if (other.gameObject.layer == 6) {
            HandleObjectState(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    private void HandleObjectState(GameObject other)
    {
        NPCCombat combat;
        other.TryGetComponent<NPCCombat>(out combat);
        if (combat == null) {
            Debug.LogError("collider is not found.");
        }
        
        if (combat.jamSpawned) {
            Stats.AddPoint();
        }
        else {
            Stats.RemovePoint();
        }
    }
}
