using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace NPC.Generator {
    public class NPCGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject BreadObject;
        [SerializeField] private float baseTimeToSpawn = 10f;
        private float timePassedSinceLastSpawn = 0f;
        private float actualTimeToSpawn;
        private float spawnCoords;
        [SerializeField] private float HalfSpawnWidth = 300f;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Every 10 seconds the level increases (difficulty/spawn rate increase)
            UpdateTimeToSpawn();
            
            if (CheckTimePassed()) {
                SpawnNPC();
            }
        }

        private void UpdateTimeToSpawn()
        {
            actualTimeToSpawn = baseTimeToSpawn - Stats.Level;
            if (actualTimeToSpawn < 2f) {
                actualTimeToSpawn = 2f;
            } 
        }

        private void SpawnNPC()
        {
            spawnCoords = GetRandomCoords();
            Instantiate(BreadObject, new Vector3(transform.position.x + spawnCoords, transform.position.y, transform.position.z), quaternion.identity);
        }
        private float GetRandomCoords()
        {
            return UnityEngine.Random.Range(-HalfSpawnWidth, HalfSpawnWidth);
        }

        private bool CheckTimePassed()
        {
            timePassedSinceLastSpawn += Time.deltaTime;
            
            if (actualTimeToSpawn <= timePassedSinceLastSpawn) {
                ResetTimerToSpawn();
                return true;
            }
            return false;

        }

        private void ResetTimerToSpawn()
        {
            timePassedSinceLastSpawn = 0f;
        }
    }
}