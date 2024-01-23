using System;
using Player;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Combat {
    public class PlayerCombat : MonoBehaviour {
        [SerializeField] private LayerMask EnemyLayer;
        [SerializeField] private Vector3 SpawnJamOnToastCoords;
        [SerializeField] private float reloadTime = 2f;
        [SerializeField] private float timeBetweenAttacks = 1.5f;
        [SerializeField] private int maxAmmoAmount = 3;

        private PlayAnimationManager playerAnim;
        
        // Actual amount of ammo
        private int ammoActual;

        // Shooting ray
        // private Vector3 shootDirection;
        // private Vector3 shootOrigin = new(0, 0.5f, 0);

        // Attack delays
        private bool isReloading = false;
        private bool canAttack = true;
        private bool hasAttacked = false;

        // Timers
        private Globals.Timer attackTimer = new();
        private Globals.Timer reloadTimer = new();

        // Attack collision box
        Vector3 attackBoxDimensions = new(4f/2, 1.5f/2, 3f/2);
        float attackBoxZCenterOffset = 2.25f;

        // Allocating memory for attacking collision
        Collider[] hits = new Collider[20];
        void Start() {
            ammoActual = maxAmmoAmount;
            playerAnim = gameObject.GetComponent<PlayAnimationManager>();
        }
        
        void Update() {
            HandleTimers();

            HandleReload();
            HandleAttack();

            HandleAnimations();
        }

        private void HandleAnimations()
        {
            if (isReloading) {
                playerAnim.SetCombatState("isReloading", true);
            }
            else {
                playerAnim.SetCombatState("isReloading", false);
            }
            
            if (hasAttacked) {
                playerAnim.SetCombatState("isAttacking", true);
            }
            else {
                playerAnim.SetCombatState("isAttacking", false);
            }
        }

        private void HandleTimers()
        {
            if (!canAttack) {
                hasAttacked = false; // Probably not a good spot for this
                attackTimer.AddTime(Time.deltaTime);
                
                //Debug.Log(attackTimer.timePassed);
                if (attackTimer.timePassed >= timeBetweenAttacks) {
                    //Debug.Log("Can attack again!");
                    
                    canAttack = true;
                    attackTimer.Reset();
                }
            }

            if (isReloading) {
                reloadTimer.AddTime(Time.deltaTime);
                Debug.Log(reloadTimer.timePassed);
                
                if (reloadTimer.timePassed >= reloadTime) {
                    //Debug.Log("Ended Reload!");
                    
                    isReloading = false;
                    ResetAmmo();
                    reloadTimer.Reset();
                }
            }
        }

        private void ResetAmmo()
        {
            ammoActual = maxAmmoAmount;
        }

        private void HandleAttack()
        {
            Attack();
            HandleBetweenAttacks();
        }

        private void Attack()
        {
            if (Input.GetMouseButtonDown(0)) { // 0 is Left mouse button
                if (canAttack && !isReloading) {
                    //Debug.Log("Attacked!");
                    
                    ammoActual -= HandleAttackBox();
                    hasAttacked = true;
                    canAttack = false;
                }
            }
        }

        private int HandleAttackBox()
        {
            int amountJammed = 0;

            // TODO: Move the collision dimensions to another file
            Physics.OverlapBoxNonAlloc(transform.forward * attackBoxZCenterOffset + transform.position,
                attackBoxDimensions, hits, transform.rotation, EnemyLayer);

            // Iterate over all enemies hit
            for (int i = 0; i < hits.Length; i++) {
                
                if (hits[i] == null) {
                    Debug.Log("SegFault!");
                    break;
                }

                // Get the needed NPC Combat script from each
                NPCCombat npcTmp = hits[i].gameObject.GetComponent<NPCCombat>();

                // If not jammed, jam
                if (!npcTmp.jamSpawned) {
                    npcTmp.JamIt(SpawnJamOnToastCoords);
                    
                    amountJammed++;
                }
            }
            
            Array.Clear(hits, 0, hits.Length);
            return amountJammed;
        }

        private void HandleBetweenAttacks()
        {
            if (!canAttack) {
                if (attackTimer.timePassed >= timeBetweenAttacks) {
                    
                    // Reset timer and enable attacking
                    canAttack = true;
                    attackTimer.Reset();
                }
            }
        }

        private void HandleReload()
        {
            if (ammoActual <= 0 || Input.GetAxis("Fire2") > 0) {
                //Debug.Log("Started reloading!");
                isReloading = true;
            }
        }

        // private void HandleRayCast()
        // {
        //     RaycastHit hitInfo;
        //     shootDirection = transform.TransformDirection(Vector3.forward); 
            
        //     bool hit = Physics.Raycast(transform.position - shootOrigin, shootDirection, out hitInfo, shootRange, EnemyLayer);
        //     Debug.DrawRay(transform.position - shootOrigin, shootDirection * shootRange, Color.green, 5f);

        //     if (hit) {
        //         NPCCombat npcC = hitInfo.collider.gameObject.GetComponent<NPCCombat>();
        //         //Debug.Log("hit");

        //         if (!npcC.jamSpawned) {
        //             npcC.JamIt(SpawnJamOnToastCoords);
        //         }
        //     }
        // }

        internal int GetCurrentAmmo() {
            return ammoActual;
        }
    }
}