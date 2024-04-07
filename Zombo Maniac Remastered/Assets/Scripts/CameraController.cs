using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public Transform CameraTransform;
    
    [Header("Handle Movement WASD")]
    public float movementSpeed;
    public float normalSpeed;
    public float fastSpeed;
    public float movementTime;
    public float rotationAmount;

    [Header("Handle Zoom")] 
    public float zoomSpeed;
    public Vector3 zoomAmount;

    [Header("Zoom Limits")]
    public float minZoomLevel = 10f; // Minimum zoom level
    public float maxZoomLevel = 50f; // Maximum zoom level


    [Header("Position")]
    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;
    
    
    
    private void Start()
    {

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = CameraTransform.localPosition;
    }

    private void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
    }

    void HandleMouseInput()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            // Adjust the potentialNewZoom based on scroll input and zoomAmount
            Vector3 potentialNewZoom = newZoom + (zoomAmount * scrollInput * zoomSpeed);

            // Assuming zoom is primarily along the Z-axis, enforce min and max zoom levels
            // This assumes your zoomAmount.z is negative for zooming in and positive for zooming out
            // Adjust the condition if your camera's zoom direction is set up differently
            if ((newZoom.z + (zoomAmount.z * scrollInput * zoomSpeed)) > -maxZoomLevel && 
                (newZoom.z + (zoomAmount.z * scrollInput * zoomSpeed)) < -minZoomLevel)
            {
                newZoom = potentialNewZoom;
            }
            // Additionally, you might want to clamp within the bounds if exceeded
            newZoom.z = Mathf.Clamp(newZoom.z, -maxZoomLevel, -minZoomLevel);
        }
    }



    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            newPosition += (transform.forward * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            newPosition += (transform.forward * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        
        
        //Smoothing the camera movement
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        CameraTransform.localPosition = Vector3.Lerp(CameraTransform.localPosition, newZoom, Time.deltaTime * zoomSpeed);
    }
    
}
