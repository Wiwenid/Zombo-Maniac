using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public ObjectPool objectPool; 
    public string currentPrefabTag;
    private Vector3 currentPos;

    private void Update()
    {
        currentPos = transform.position;
    }


    public void switchState()
    {
        objectPool.SpawnFromPool(currentPrefabTag,currentPos, Quaternion.identity);
        
        this.gameObject.SetActive(false);
    }
}
