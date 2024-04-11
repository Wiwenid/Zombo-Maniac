using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private void Start()
    {
        UnittSelections.Instance.unitList.Add(this.gameObject);
    }
}
