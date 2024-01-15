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
        int level {get; set;} = 1;
        private float spawnCoords;

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Every 10 seconds the level increases (difficulty/spawn rate increase)
            UpdateLevel();
            UpdateTimeToSpawn();
            
            if (CheckTimePassed()) {
                SpawnNPC();
            }
        }

        private void UpdateTimeToSpawn()
        {
            actualTimeToSpawn = baseTimeToSpawn - level;
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
            return UnityEngine.Random.Range(-250f, 250f);
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

        private void UpdateLevel()
        {
            level = (int)Timer.TotalTimePassed / 10;
            if (level < 1) level = 1;
        }
    }
}