using System.Collections.Generic;
using UnityEngine;

public class AoEDamage : MonoBehaviour
{
    public AttributesManager atm;
    public string damageableEntityTag;
    public float damageInterval = 1f; // Damage once per second

    private Dictionary<GameObject, float> lastDamageTimes = new Dictionary<GameObject, float>();

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(damageableEntityTag))
        {
            float currentTime = Time.time;
            if (!lastDamageTimes.ContainsKey(other.gameObject))
            {
                lastDamageTimes.Add(other.gameObject, 0f);
            }
            if (currentTime >= lastDamageTimes[other.gameObject] + damageInterval)
            {
                AttributesManager targetAtm = other.GetComponent<AttributesManager>();
                if (targetAtm != null)
                {
                    targetAtm.TakeDamage(atm.attack);
                    lastDamageTimes[other.gameObject] = currentTime;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(damageableEntityTag) && lastDamageTimes.ContainsKey(other.gameObject))
        {
            lastDamageTimes.Remove(other.gameObject);
        }
    }
}