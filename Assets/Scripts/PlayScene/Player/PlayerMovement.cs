using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody playerRigidbody;
    [SerializeField] private Transform playerCam;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    [Header("Dash")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 2f;
    private Vector3 moveDirection;
    private float rotationVelocity;
    private bool isDashing = false;
    private float dashTimer = 0f;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody == null)
        {
            Debug.LogError("No Rigidbody component found on the player.");
        }
    }

    private void FixedUpdate()
    {
        if (playerRigidbody != null)
        {
            /* Debug.Log("is it dashing? " + isDashing + ""); */

            if (!isDashing)
            {
                RotatePlayerModelToMouse();
                MovePlayer();
                playerRigidbody.velocity = moveDirection;
            } else if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
            {
                StartCoroutine(Dash());
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

    private IEnumerator Dash()
    {
        isDashing = true;

        Vector3 dashVelocity = moveDirection * dashDistance / dashDuration;
        Vector3 originalVelocity = playerRigidbody.velocity;

        float dashStartTime = Time.time;

        while (Time.time < dashStartTime + dashDuration)
        {
            float ratio = (Time.time - dashStartTime) / dashDuration;
            playerRigidbody.velocity = Vector3.Lerp(originalVelocity, dashVelocity, ratio);
            yield return null;
        }

        playerRigidbody.velocity = originalVelocity;
        isDashing = false;
    }
}