using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController playerController;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float gravity = -9.81f;
    private bool isTouchingGround;

    private Vector3 moveDirection;
    private float rotationVelocity;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
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
            

            if (isTouchingGround && Input.GetButtonDown("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if(isTouchingGround && !Input.GetButtonDown("Jump")){
                moveDirection.y = -1f;
            }
            else if (!isTouchingGround)
            {
                moveDirection.y += gravity * Time.deltaTime;
            }

            playerController.Move(moveDirection * Time.deltaTime);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isTouchingGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isTouchingGround = false;
        }
    }
}