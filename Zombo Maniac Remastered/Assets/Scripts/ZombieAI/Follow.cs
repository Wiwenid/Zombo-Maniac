using UnityEngine;

public class Follow : MonoBehaviour
{
    public string targetTag = "Player"; // Tag of GameObjects to follow
    public float speed = 5f; // Movement speed towards the target
    private Transform target; // Current target to follow
    private bool shouldFollow = false; // Flag to determine if the follow behavior is active

    void Update()
    {
        // Only the parent GameObject moves towards the target
        if (shouldFollow && target != null)
        {
            // Calculate the step size
            Vector3 step = speed * Time.deltaTime * (target.position - transform.parent.position).normalized;
            // Move the parent GameObject towards the target
            transform.parent.position += step;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            target = other.transform; // Set the target to follow
            shouldFollow = true; // Activate following behavior
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == target) // If the exiting object is the target
        {
            shouldFollow = false; // Stop following
            target = null; // Clear the target
        }
    }
}