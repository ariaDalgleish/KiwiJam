﻿using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera zoomCamera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;


    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        zoomCamera = GetComponent<Camera>();
        if (GetComponent<Camera>())
        {
            defaultFOV = GetComponent<Camera>().fieldOfView;
        }
    }

    void Update()
    {
        // Update the currentZoom and the camera's fieldOfView.
        currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
