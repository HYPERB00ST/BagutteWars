using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC.Generator {
    public class NPCGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject breadObject;
        int level = 1;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Every 10 seconds the level increases (difficulty/spawn rate increase)
            UpdateLevel();
            
            if (CheckTimePassed()) {
                SpawnNPC();
            }
        }

        private void SpawnNPC()
        {
            
        }

        private bool CheckTimePassed()
        {
            throw new NotImplementedException();
        }

        private void UpdateLevel()
        {
            throw new NotImplementedException();
        }
    }
}