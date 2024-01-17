using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NPCCombat : MonoBehaviour
{
    [SerializeField] private GameObject jamObject;
    private Vector3 JamCoords;
    private bool jammed = false;
    internal bool jamSpawned {get; private set;} = false;

    void Update() {
        CheckJamState();
    }

    private void CheckJamState()
    {
        if (CheckJam()) {
            if (!jamSpawned) {
                SpawnJam();
            }
        }
    }

    private void SpawnJam()
    {
        Instantiate(jamObject, JamCoords, quaternion.identity, gameObject.transform);
        jamSpawned = true;
    }

    private bool CheckJam()
    {
        if (jammed) {
            return true;
        }
        return false;
    }

    internal void JamIt(Vector3 coords) {
        Debug.Log("jammed!");
        jammed = true;

        // Debug for jam coords
        JamCoords = transform.TransformPoint(coords);
    }
}
