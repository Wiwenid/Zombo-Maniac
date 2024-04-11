using UnityEngine;
using UnityEngine.AI;

public class FleeToSafety : MonoBehaviour
{
    public string safeTag = "Safe"; // Tag for safe places
    private NavMeshAgent agent;
    private AttributesManager attributesManager; // Access to health
    private bool isFleeing = false; // To prevent multiple flee attempts

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        attributesManager = GetComponent<AttributesManager>();

        if (agent == null)
        {
            Debug.LogError($"{gameObject.name} requires a NavMeshAgent component.", this);
        }

        if (attributesManager == null)
        {
            Debug.LogError($"{gameObject.name} requires an AttributesManager component.", this);
        }
    }

    void Update()
    {
        if (!isFleeing && ShouldFlee())
        {
            Flee();
        }
    }

    private bool ShouldFlee()
    {
        // Checks if health is 0 or below and not currently fleeing
        return attributesManager != null && attributesManager.health <= 0;
    }

    private void Flee()
    {
        isFleeing = true;
        GameObject closestSafePlace = FindClosestSafePlaceWithTag(safeTag);

        if (closestSafePlace != null)
        {
            agent.SetDestination(closestSafePlace.transform.position);
            Debug.Log($"{gameObject.name} is fleeing to {closestSafePlace.name}");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} cannot find a safe place to flee to!");
            isFleeing = false; // Reset fleeing status if no safe place found
        }
    }

    private GameObject FindClosestSafePlaceWithTag(string tag)
    {
        GameObject[] safePlaces = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject safePlace in safePlaces)
        {
            float distance = Vector3.Distance(transform.position, safePlace.transform.position);
            if (distance < closestDistance)
            {
                closest = safePlace;
                closestDistance = distance;
            }
        }

        return closest;
    }
}
