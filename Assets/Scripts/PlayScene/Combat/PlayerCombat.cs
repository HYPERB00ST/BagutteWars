using System;
using UnityEngine;

namespace Combat {
    public class PlayerCombat : MonoBehaviour {
        [SerializeField] private float ShootRange = 50f;
        [SerializeField] private float testoffset = 10f;
        [SerializeField] private LayerMask EnemyLayer;
        [SerializeField] private Vector3 DebugJamCoords;
        private Vector3 shootDirection;
        void Update() {
            HandleAttack();
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
            
            bool hit = Physics.Raycast(transform.position + new Vector3(0, 0, testoffset), shootDirection, out hitInfo, ShootRange, EnemyLayer);
            Debug.DrawRay(transform.position + new Vector3(0, 0, testoffset), shootDirection * ShootRange, Color.green, 5f);

            if (hit) {
                Debug.Log("hit");
                hitInfo.collider.gameObject.GetComponent<NPCCombat>().JamIt(DebugJamCoords);
            }
        }
    }
}