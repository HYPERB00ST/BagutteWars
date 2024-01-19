using UnityEngine;

namespace Combat {
    public class PlayerCombat : MonoBehaviour {
        [SerializeField] private LayerMask EnemyLayer;
        [SerializeField] private Vector3 SpawnJamOnToastCoords;
        [SerializeField] private float shootRange = 50f;
        [SerializeField] private float reloadTime = 2f;
        [SerializeField] private float timeBetweenAttacks = 1.5f;
        [SerializeField] private int maxAmmoAmount = 3;
        
        // Actual amount of ammo
        private int ammoActual;

        // Shooting ray
        private Vector3 shootDirection;
        private Vector3 shootOrigin = new(0, 0.5f, 0);

        // Attack delays
        private bool isReloading = false;
        private bool canAttack = true;

        // Timers
        private Globals.Timer attackTimer = new();
        private Globals.Timer reloadTimer = new();

        void Start() {
            ammoActual = maxAmmoAmount;
        }
        
        void Update() {
            HandleTimers();

            HandleReload();
            HandleAttack();
        }

        private void HandleTimers()
        {
            if (!canAttack) {
                attackTimer.AddTime(Time.deltaTime);
                
                if (attackTimer.timePassed >= timeBetweenAttacks) {
                    
                    canAttack = true;
                    attackTimer.Reset();
                }
            }

            if (isReloading) {
                reloadTimer.AddTime(Time.deltaTime);
                
                if (reloadTimer.timePassed >= reloadTime) {
                    
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
                    HandleRayCast();
                    
                    canAttack = false;
                    ammoActual -= 1;
                }
            }
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
            if (ammoActual <= 0) {
                isReloading = true;
            }
        }

        private void HandleRayCast()
        {
            RaycastHit hitInfo;
            shootDirection = transform.TransformDirection(Vector3.forward); 
            
            bool hit = Physics.Raycast(transform.position - shootOrigin, shootDirection, out hitInfo, shootRange, EnemyLayer);
            Debug.DrawRay(transform.position - shootOrigin, shootDirection * shootRange, Color.green, 5f);

            if (hit) {
                NPCCombat npcC = hitInfo.collider.gameObject.GetComponent<NPCCombat>();
                Debug.Log("hit");

                if (!npcC.jamSpawned) {
                    npcC.JamIt(SpawnJamOnToastCoords);
                }
            }
        }
    }
}