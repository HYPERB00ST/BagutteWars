using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    private CharacterController characterController;
    [SerializeField] private Transform playerCam;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    [Header("Dash")]
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private float dashCooldown = 2f;
    private Vector3 moveDirection;
    private float rotationVelocity;
    private bool isDashing = false;
    private float dashTimer = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("No CharacterController component found on the player.");
        }
    }

    private void FixedUpdate()
    {
            /* Debug.Log("is it dashing? " + isDashing + ""); */

        if (!isDashing)
        {
            RotatePlayerModelToMouse();
            MovePlayer();
            characterController.Move(moveDirection * Time.fixedDeltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Dash!");
            characterController.Move(moveDirection * (Time.fixedDeltaTime + dashSpeed));
            isDashing = true;
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashCooldown)
            {
                isDashing = false;
                dashTimer = 0f;
            }
        }
    }

    private void RotatePlayerModelToMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (playerPlane.Raycast(ray, out float hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            targetPoint.y = transform.position.y;

            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            float currentAngle = gameObject.transform.eulerAngles.y;
            float targetAngle = targetRotation.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref rotationVelocity, rotationSmoothTime);

            gameObject.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ);
        move = move.normalized;
        move *= movementSpeed;
        moveDirection = move;
    }
}