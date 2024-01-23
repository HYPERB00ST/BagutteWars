using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] private CharacterController player;
    [SerializeField] private float gravityStrength = 9.81f;
    [SerializeField] private LayerMask GroundMask;
    private float gravityActual = 0f;
    private bool onGround = true;
    private Vector3 gravityCheckVector;
    Vector3 checkPosition;

    void Start() {
        checkPosition = transform.position;
        checkPosition.y -= 1.08f;

        gravityCheckVector = new(0.74f/2, 0.11f/2, 0.47f/2);
    }

    private bool CheckGravity()
    {
        return Physics.CheckBox(checkPosition, gravityCheckVector, quaternion.identity, GroundMask, QueryTriggerInteraction.UseGlobal);
    }

    void FixedUpdate()
    {
        onGround = CheckGravity();
        
        if (onGround) {
            gravityActual = 2f;
        }
        else {
            // Time squared accel formula
            gravityActual += gravityStrength * Time.fixedDeltaTime;
        }
        player.Move(gravityActual * Vector3.down * Time.fixedDeltaTime);
    }
}
