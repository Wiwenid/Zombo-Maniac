using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSpawner : MonoBehaviour
{
    public ObjectPool objectPool; // Reference to your ObjectPool script
    private string currentPrefabTag = "Zombie"; // Default spawnable object tag
    public LayerMask spawnAble;
    public GameObject spawnPoint;


    private void OnDisable()
    {
        spawnPoint.SetActive(false);
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // Exit the Update method if clicking over a UI element
        }
        
        UpdateCursorPositionParticle();
        
        if (Input.GetMouseButtonDown(0))
        {
            SpawnFromCurrentPrefab();
        }

    }

    void SpawnFromCurrentPrefab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, spawnAble))
        {
            objectPool.SpawnFromPool(currentPrefabTag, hit.point, Quaternion.identity);
 
        }
    }

    // Called by UI buttons to change the current spawning prefab
    public void ChangePrefabToSpawn(string prefabTag)
    {
        currentPrefabTag = prefabTag;
    }
    
    void UpdateCursorPositionParticle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, spawnAble))
        {
            // Move the particle system to the point where the cursor is pointing
            spawnPoint.transform.position = hit.point;  
            spawnPoint.SetActive(true);
        }
        else
        {
            spawnPoint.SetActive(false);
        }
    }
    
}