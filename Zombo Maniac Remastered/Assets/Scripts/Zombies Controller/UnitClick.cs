using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    public GameObject groundMarker;

    public LayerMask clickable;
    public LayerMask ground;

    private void Start()
    {
        myCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                //if we hit a clickable object

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //shift click
                    UnittSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    //normal click
                    UnittSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
                
            }
            else
            {
                //if we didn't

                if (!Input.GetKeyDown(KeyCode.LeftShift))
                {
                    UnittSelections.Instance.DeselectAll();
                }
                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(true);
            }
        }
    }
}
