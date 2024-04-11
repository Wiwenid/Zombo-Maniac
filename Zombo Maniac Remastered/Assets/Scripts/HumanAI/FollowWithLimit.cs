using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

public class FollowWithLimit : MonoBehaviour
{
    private NavMeshAgent _agent;
    public float searchRadius;
    public string targetTag;
    public string safeZone;
    private float timer = 0f; // Timer to track the elapsed time
    public float zomTimer = 5f; // Target time in seconds to trigger a method
    

    private AttributesManager _atm;
    private Vector3 _originalPos;
    private bool isFleeing = true;
    private ChangeState turnZom;
    
    
    private void Start()
    {
        _originalPos = transform.position;
        _agent = GetComponent<NavMeshAgent>();
        _atm = GetComponent<AttributesManager>();
        turnZom = GetComponent<ChangeState>();
    }

    private void Update()
    {
        Debug.Log(timer);
        float health = _atm.health;
        Debug.Log(health);
        if (health > 0 && isFleeing)
        {
            HandleReturn();
            Debug.Log("Alive");
        }
        else
        {
            HandleFleeing();
            Debug.Log("Dead");
            timer += Time.deltaTime;
            becomeZombie();
        }
        
    }

    void HandleFollow()
    {
        GameObject target = FindClosestWithTag(targetTag);
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= searchRadius)
        {
            _agent.SetDestination(target.transform.position);
        }
    }

    void HandleReturn()
    {
        _agent.SetDestination(_originalPos);
        HandleFollow();
    }

    void HandleFleeing()
    {
        isFleeing = false;
        GameObject safeZone = FindClosestWithTag(this.safeZone);
        _agent.SetDestination(safeZone.transform.position);
        float health = _atm.health;
        float maxHealth = _atm.maxHealth;
        if (health >= maxHealth)
        {
            isFleeing = true;
            timer = 0;
        }
    }
    
    GameObject FindClosestWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closest = obj;
                closestDistance = distance;
            }
        }

        return closest;
    }

    void becomeZombie()
    {
        if (timer >= zomTimer)
        {
            turnZom.switchState();
        }
    }

}

