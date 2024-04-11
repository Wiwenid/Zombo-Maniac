using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSwitcher : MonoBehaviour
{
    public GameObject zombieSpawner;

    
    private bool isSpawingMode = true;

    private void Update()
    {
        HandlingSwitch();
    }

    void HandlingSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isSpawingMode = !isSpawingMode;
            
            zombieSpawner.SetActive(!isSpawingMode);

        }
    }
}
