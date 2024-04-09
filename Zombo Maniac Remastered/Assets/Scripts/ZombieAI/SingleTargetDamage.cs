using UnityEngine;

public class SingleTargetDamage : MonoBehaviour
{
    public AttributesManager atm;
    public string damageableEntityTag;
    public float damageInterval = 1f; // Damage once per second

    private GameObject currentTarget;
    private float lastDamageTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (currentTarget == null && other.CompareTag(damageableEntityTag))
        {
            currentTarget = other.gameObject;
        }

        if (other.gameObject == currentTarget && Time.time >= lastDamageTime + damageInterval)
        {
            AttributesManager targetAtm = other.GetComponent<AttributesManager>();
            if (targetAtm != null)
            {
                targetAtm.TakeDamage(atm.attack);
                lastDamageTime = Time.time;
                Debug.Log($"Damage applied to {other.gameObject.name} as single target.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTarget)
        {
            currentTarget = null; // Clear the target when it exits the trigger zone
        }
    }
}