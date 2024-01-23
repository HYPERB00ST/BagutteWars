using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayAnimationManager : MonoBehaviour
    {
        private Animator playerAnimator;
        internal PlayerStateInfo.State CurrentState{get; private set;}
        void Start()
        {
            playerAnimator = GetComponentInChildren<Animator>();
            if (!playerAnimator) {
                Debug.LogError("No Animator on player!");
            }
        }

        void Update()
        {
            UpdateAnimatorMoveState();
        }

        private void UpdateAnimatorMoveState()
        {
            playerAnimator.SetInteger("moveState", (int)CurrentState);
        }

        internal void SetCurrentMoveState(PlayerStateInfo.State state) {
            CurrentState = state;
        }
        internal void SetCombatState(string boolName, bool boolValue) {
            playerAnimator.SetBool(boolName, boolValue);
        }
    }
}
