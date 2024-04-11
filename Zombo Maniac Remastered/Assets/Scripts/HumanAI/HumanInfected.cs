using UnityEngine;
using UnityEngine.AI;

public class HumanInfected : MonoBehaviour
{
    public string safeTag = "Safe"; // Tag for safe places
    private NavMeshAgent agent;
    private AttributesManager attributesManager; // Access to health
    public bool isFleeing = false; // To prevent multiple flee attempts
    private GameObject currentDestination;

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
        else if (isFleeing && !IsDestinationReached())
        {
            // Optional: re-evaluate fleeing if the agent gets stuck or the destination becomes unreachable.
            TryUpdateFleeDestination();
        }
    }

    private bool ShouldFlee()
    {
        // Checks if health is 0 or below and not currently fleeing
        return attributesManager != null && attributesManager.health <= 0 && !agent.pathPending;
    }

    private void Flee()
    {
        isFleeing = true;
        GameObject closestSafePlace = FindClosestSafePlaceWithTag(safeTag);

        if (closestSafePlace != null)
        {
            agent.SetDestination(closestSafePlace.transform.position);
            currentDestination = closestSafePlace;
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

    private bool IsDestinationReached()
    {
        // Check if the agent has reached the destination or is close enough
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void TryUpdateFleeDestination()
    {
        if (IsDestinationReached() || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            isFleeing = false; // Attempt to flee again which will recalculate the destination
        }
    }
}
