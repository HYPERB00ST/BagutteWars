using System;
using Player;
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
    private Vector3 moveFinal;
    private PlayAnimationManager animationManager;
    private GameObject cameraObj;
    private bool isMoving = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("No CharacterController component found on the player.");
        }

        cameraObj = GameObject.Find("Main Camera");
        animationManager = gameObject.GetComponent<PlayAnimationManager>();
    }

    private void FixedUpdate()
    {
            /* Debug.Log("is it dashing? " + isDashing + ""); */

        if (!isDashing)
        {
            RotatePlayerModelToMouse();
            MovePlayer();
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

    void Update() {

        HandleAnimatorState();
    }

    private void HandleAnimatorState()
    {
        if (!isMoving) {
            animationManager.SetCurrentMoveState(PlayerStateInfo.State.Idle);
            return;
        }
        float directionAngle = Vector3.Angle(transform.forward, moveFinal);
        switch (directionAngle) {
            
            case float i when i <= 45f:
                animationManager.SetCurrentMoveState(PlayerStateInfo.State.Forward);
                break;
            case float i when i <= 135f && i > 45f:
                    animationManager.SetCurrentMoveState(PlayerStateInfo.State.Right);
                break;
            case float i when i > 135f:
                animationManager.SetCurrentMoveState(PlayerStateInfo.State.Back);
            break;
            default:
                animationManager.SetCurrentMoveState(PlayerStateInfo.State.Idle);
                break;
        }
    }

    private void RotatePlayerModelToMouse()
    {
        Plane playerPlane = new(Vector3.up, transform.position);
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
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        if (moveZ != 0 || moveX != 0) {
            isMoving = true;
        }
        else {
            isMoving = false;
            return;
        }
        
        moveFinal = (cameraObj.transform.up.normalized * moveZ + cameraObj.transform.right.normalized * moveX) * movementSpeed;
        moveFinal.y = 0f;
        characterController.Move(moveFinal * Time.fixedDeltaTime);
    }
}