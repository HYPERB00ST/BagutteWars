using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSmoothTime = 0.1f;

    private Vector3 moveDirection;
    private float rotationVelocity;

    void Start()
    {
        if (playerController == null)
        {
            Debug.LogError("CharacterController is not assigned. Please assign it in the inspector.");
        }
    }

    void Update()
    {
        if (playerController != null)
        {
            RotatePlayerModelToMouse();
            MovePlayer();
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
        float moveY = Input.GetAxis("Vertical");

        moveDirection = movementSpeed * Time.deltaTime * (moveX * Vector3.right + moveY * Vector3.forward);

        playerController.Move(moveDirection);
    }
}