using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace NPC.Generator {
    public class NPCGenerator : MonoBehaviour
    {
        [SerializeField] private float BASE_TIME_TO_SPAWN = 11f;
        [SerializeField] private float HALF_SPAWN_WIDTH = 17f;
        [SerializeField] private GameObject BreadObject;
        private float timePassedSinceLastSpawn = 0f;
        private float actualTimeToSpawn;
        private float spawnCoords;
        
        void Start()
        {
            StartCoroutine(GenerateNPCs());
        }

        void Update() {
            UpdateTimeToSpawn();
        }

        IEnumerator GenerateNPCs()
        {
            while (true) {
                if (CheckTimePassed()) {
                    SpawnNPC();
                    yield return new WaitForSeconds(actualTimeToSpawn);
                }
                yield return null;
            }
        }

        private void UpdateTimeToSpawn()
        {
            // Every 10 seconds the level increases (difficulty/spawn rate increase)
            actualTimeToSpawn = BASE_TIME_TO_SPAWN - Stats.Level;
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
            return UnityEngine.Random.Range(-HALF_SPAWN_WIDTH, HALF_SPAWN_WIDTH);
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