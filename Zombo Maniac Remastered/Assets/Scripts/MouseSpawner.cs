using UnityEngine;

public class MouseSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnerObject; // Assign in Unity Editor
    private IEntitySpawner entitySpawner;
    public LayerMask spawnableMask;

    void Start()
    {
        if (spawnerObject != null)
        {
            entitySpawner = spawnerObject.GetComponent<IEntitySpawner>();
            if (entitySpawner == null)
            {
                Debug.LogError("The specified GameObject does not have a component that implements IEntitySpawner.");
            }
        }
        else
        {
            Debug.LogError("Spawner GameObject is not assigned.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && entitySpawner != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, spawnableMask))
            {
                entitySpawner.SpawnEntity(hit.point);
            }
        }
    }
    
    
}