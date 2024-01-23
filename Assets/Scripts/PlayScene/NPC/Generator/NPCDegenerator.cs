using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDegenerator : MonoBehaviour
{

    private new Collider collider;
    private Stats statsRef;
    void Start()
    {
        gameObject.TryGetComponent<Collider>(out collider);
        if (collider == null)
        {
            Debug.LogError("NPCDeg/collider is missing.");
        }

        GameObject.Find("Stats").TryGetComponent<Stats>(out statsRef);
        if (statsRef == null) {
            Debug.LogError("NPCDeg/statsRef is missing.");
        }
    }

    void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger");

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
            statsRef.AddPoint();
        }
        else {
            statsRef.ShortenTimeToPlay();
        }
    }
}
