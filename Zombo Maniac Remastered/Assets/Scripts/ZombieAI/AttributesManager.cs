using System;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI; // For UI elements

public class AttributesManager : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public float maxHealth = 100;
    public int attack;
    
    [Header("UI")]
    public Image healthBarFill; // Reference to the filled Image component for health
    private Camera mainCamera;
    
    public GameObject HealthUi; // The GameObject that contains your health UI
    private NavMeshAgent agent;
    public bool isHuman;


    void OnEnable()
    {
        health = maxHealth; // Initialize health to max at start
        mainCamera = Camera.main; // Cache the main camera reference
        agent = GetComponent<NavMeshAgent>(); // Ensure this component is attached
        UpdateHealthUI(); // Update the health UI to reflect the starting health
        
    }

    private void Update()
    {
        HandleReset();
    }

    void HandleReset()
    {
        if (health <= 0)
        {
            if (isHuman)
            {
                
            }
            else
            {
                // If it's a zombie, just disable the GameObject
                gameObject.SetActive(false);
            }
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth); // Ensure health doesn't drop below 0
        UpdateHealthUI(); // Update the health UI to reflect the new health value
        
        
    }

    // This method would be called to deal damage to a target
    public void DealDamage(GameObject target)
    {
        AttributesManager targetAttributes = target.GetComponent<AttributesManager>();
        if (targetAttributes != null)
        {
            targetAttributes.TakeDamage(attack);
        }
    }

    // Updates the health bar UI based on current health
    private void UpdateHealthUI()
    {
        if (healthBarFill != null) // Check if the health bar Image component is assigned
        {
            healthBarFill.fillAmount = (float)health / maxHealth; // Calculate fill amount
        }
    }


    
    private void LateUpdate()
    {
        // Ensure the health UI always faces the camera
        if (HealthUi != null && mainCamera != null)
        {
            HealthUi.transform.LookAt(HealthUi.transform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }
    }
}
