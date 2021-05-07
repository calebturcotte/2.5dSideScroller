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

    private GameObject lastPlatform;
    public GameObject grappleObject;

    LayerMask movingPlatform = 12;
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

    private void OnCollisionEnter(Collision collision) //for universal character collision interactions, see character script
    {

        bool grounded = IsGrounded();

        if (collision.gameObject.tag == "MovingPlatform" && grounded) // if the collision is with a platform and you're ON TOP
        {
            transform.SetParent(collision.gameObject.transform);
        }
        else
        {
            //Debug.Log(collision.gameObject.tag);
            transform.SetParent(null);
        }


        //save the last platform we were on
        if((collision.gameObject.CompareTag("MovingPlatform") || collision.gameObject.CompareTag("Platform")) && grounded)
        {
            lastPlatform = collision.gameObject;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        this.transform.SetParent(null); //unparent character when not colliding with anything
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


    //Returns the last platform the Player stood on
    public GameObject getLastPlatform()
    {
        return lastPlatform;
    }


}

