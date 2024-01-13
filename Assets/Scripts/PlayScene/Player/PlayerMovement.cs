using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController playerController;
        [SerializeField] private float movementSpeed;
        Vector3 MoveX;
        Vector3 MoveY;
        Vector3 FinalMove;
        
        void Start()
        {

        }

        void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            MoveX = Input.GetAxis("Horizontal") * transform.right;
            MoveY = Input.GetAxis("Vertical") * transform.forward;

            FinalMove = Time.deltaTime * movementSpeed * (MoveX + MoveY);

            playerController.Move(FinalMove);
        }
    }   
}