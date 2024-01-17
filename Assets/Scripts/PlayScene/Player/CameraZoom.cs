using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 2.0f;
    [Header("Min Zoom")]
    [SerializeField] private float minZoom; 
    [SerializeField] private float minZoomOut; 
    [SerializeField] private float minZoomIn;
    [Header("Max Zoom")]
    [SerializeField] private float maxZoom; 
    [SerializeField] private float maxZoomOut; 
    [SerializeField] private float maxZoomIn;
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
        float targetFieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZoomIn"))
        {
            SetZoomValues(minZoomIn, maxZoomIn);
            Debug.Log("ZoomIn");
        }
        else if (other.gameObject.CompareTag("ZoomOut"))
        {
            SetZoomValues(minZoomOut, maxZoomOut);
            Debug.Log("ZoomOut");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ZoomIn"))
        {
            SetZoomValues(defaultMinZoom, defaultMaxZoom);
            Debug.Log("ZoomDefault");
        }
        else if (other.gameObject.CompareTag("ZoomOut"))
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