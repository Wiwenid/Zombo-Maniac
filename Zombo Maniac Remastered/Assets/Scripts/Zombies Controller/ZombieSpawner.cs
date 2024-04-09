using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ZombieSpawner : MonoBehaviour, IEntitySpawner
{
    [SerializeField] 
    public Vector3 spawnOffset = new Vector3(0, 1, 0);
    

    private int selectedZombieType = 0;

    public void SpawnEntity(Vector3 position)
    {
        ObjectPool.Instance.SpawnFromPool("Zombie", position, Quaternion.identity);
    }
    
    public void ChangeZombieType(int typeIndex)
    {
        selectedZombieType = typeIndex;
    }
}
