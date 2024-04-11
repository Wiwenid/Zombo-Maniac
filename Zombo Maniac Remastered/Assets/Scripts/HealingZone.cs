using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    private AttributesManager atm;
    public string HealingForWhoTag;
    public float _healingInterval;
    public float healAmount = 10;
    public float healMultiplier;
    
    private GameObject _currentTarget;
    private float _lastHealTime = 0f;
    private void OnTriggerStay(Collider other)
    {
        if (_currentTarget == null && other.CompareTag(HealingForWhoTag))
        {
            _currentTarget = other.gameObject;
        }

        if (other.gameObject == _currentTarget && Time.time >= _lastHealTime + _healingInterval)
        {
            AttributesManager targetAtm = other.GetComponent<AttributesManager>();
            if (targetAtm != null)
            {
                targetAtm.Heal(healAmount * healMultiplier);
                _lastHealTime = Time.time;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _currentTarget)
        {
            _currentTarget = null; // Clear the target when it exits the trigger zone
        }
    }
}
