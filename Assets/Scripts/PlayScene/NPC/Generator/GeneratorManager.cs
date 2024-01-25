using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace NPC.Generator {
    class GeneratorManager : MonoBehaviour {
        [SerializeField]
        private GameObject[] NPCSpawners;
        [SerializeField]
        private Vector3[] SpawnerCoords;
        private const int MAX_SPAWNER_AMOUNT = 2;
        private int nextSpawner = 0;
        void Start() {

        }
        void Update() {
            HandleSpawnerAmount();
        }
        private void HandleSpawnerAmount()
        {
            if (Stats.Level > 9 && nextSpawner < MAX_SPAWNER_AMOUNT) {
                SpawnNextSpawner();
                nextSpawner++;
            }
        }

        private void SpawnNextSpawner()
        {
            Instantiate(NPCSpawners[nextSpawner], SpawnerCoords[nextSpawner], quaternion.identity);
        }
    }
}