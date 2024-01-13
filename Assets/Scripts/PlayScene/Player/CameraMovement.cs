using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public Vector3 rotation;
    public float zoomSpeed = 10.0f;
    public float minZoom = 30.0f;
    public float maxZoom = 90.0f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 desiredPosition = playerTransform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredRotation = Quaternion.Euler(rotation);
            Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
            transform.rotation = smoothedRotation;

            // Zoom in and out with the mouse wheel
            float scrollData = Input.GetAxis("Mouse ScrollWheel");
            cam.fieldOfView -= scrollData * zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
        }
    }
}
