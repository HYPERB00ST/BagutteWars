using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] public float zoomSpeed = 10.0f;
    [SerializeField] public float minZoom; [SerializeField] public float minZoomOut; [SerializeField] public float minZoomIn;
    [SerializeField] public float maxZoom; [SerializeField] public float maxZoomOut; [SerializeField] public float maxZoomIn;
    [SerializeField] private Camera cam;

    private float defaultMinZoom;
    private float defaultMaxZoom;

    private void Start()
    {
        defaultMinZoom = minZoom;
        defaultMaxZoom = maxZoom;
    }

    void Update()
    {
        cam.fieldOfView -= cam.fieldOfView * zoomSpeed;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("ZoomIn"))
        {
            SetZoomValues(minZoomIn, maxZoomIn);
            Debug.Log("ZoomIn");
        }
        else if (hit.gameObject.CompareTag("ZoomOut"))
        {
            SetZoomValues(minZoomOut, maxZoomOut);
            Debug.Log("ZoomOut");
        }
        else
        {
            SetZoomValues(defaultMinZoom, defaultMaxZoom);
            Debug.Log("ZoomDefault");
        }
    }

    private void SetZoomValues(float newMinZoom, float newMaxZoom)
    {
        minZoom = newMinZoom;
        maxZoom = newMaxZoom;
    }
}