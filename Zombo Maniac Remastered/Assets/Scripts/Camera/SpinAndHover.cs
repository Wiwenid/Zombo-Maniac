using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiAndHover : MonoBehaviour
{
    public float spinSpeed = .1f;
    public float hoverHeight = 0.5f; // The maximum height of the hover
    public float hoverSpeed = 2f; // How fast the object hovers up and down

    private float originalY; // The original y-position of the object

    void Start()
    {
        // Store the original y-position of the object at start
        originalY = transform.localPosition.y;
    }

    void Update()
    {
        // Rotate the object
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime, Space.Self);

        // Calculate the new y-position using a sine wave
        float newY = originalY + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Apply the new y-position while keeping the x and z positions the same
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}