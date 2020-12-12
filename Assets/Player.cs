using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    /**
     * Our Handler for player movement
     */
    public InventoryObject inventory;
    public Vector3 mousePos;

 
    public override void Update()
    {
       
        //currently gives position of mouse on camera
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Input.mousePosition; //gives position where camera bottom left corner is 0,0
        mousePos.z = 1;

        aimingposition = (mousePos - Camera.main.WorldToScreenPoint(transform.position)).normalized * 1f;
        aimingposition.z = 0;

        base.Update();


    }


    private void Awake()
    {

        

    }


    public override void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();

        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }


    public override void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }


}
