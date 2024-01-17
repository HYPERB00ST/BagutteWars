using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Combat {
    public class PlayerCombat : MonoBehaviour {
        [SerializeField] private float ShootRange = 50f;
        [SerializeField] private LayerMask EnemyLayer;
        [SerializeField] private Vector3 DebugJamCoords;
        private Vector3 shootDirection;
        private Vector3 shootOrigin = new(0, 0.5f, 0);
        void Update() {
            HandleAttack();
            TestSceneChange();// DELETE AFTER IT WORKS
        }

        private void TestSceneChange()
        {
            if (Stats.Points >= 50) {
                SceneManager.LoadScene("ScoreScene");
            }
        }

        private void HandleAttack()
        {
            if (Input.GetMouseButtonDown(0)) {
                HandleRayCast();
            }
        }

        private void HandleRayCast()
        {
            RaycastHit hitInfo;
            shootDirection = transform.TransformDirection(Vector3.forward); 
            
            bool hit = Physics.Raycast(transform.position - shootOrigin, shootDirection, out hitInfo, ShootRange, EnemyLayer);
            Debug.DrawRay(transform.position - shootOrigin, shootDirection * ShootRange, Color.green, 5f);

            if (hit) {
                NPCCombat npcC = hitInfo.collider.gameObject.GetComponent<NPCCombat>();
                Debug.Log("hit");

                if (!npcC.jamSpawned) {
                    npcC.JamIt(DebugJamCoords);
                }
                
            }
        }
    }
}