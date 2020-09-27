using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /**
     * Our Handler for player movement
     */

    public float speed;
    public enum transitionParameter
    {
        walk,
        shoot,
        grappling,
 
    }
    public bool moveRight;
    public bool moveLeft;
    public bool grapple;
    public bool grappling;
    public bool shoot;


    public Animator animator; //var represents the animator object and controls it
   
    public float grappleSpeed = 7f;
    public Vector3 grapplePosition;
    public Rigidbody rb;
    public Camera cam;

    public Vector3 aimingposition;


    public Vector3 movement;
    // Vector3 rollmovement;
    public Vector3 mousePos;
    //   Vector3 dashmovement;



    //   Vector3 damagedPosition;
    //   private float damagedTime = 0.25f;
    // private float timetrack = 0;

    // Update is called once per frame
    void Update()
    {
       
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        //currently gives position of camera because z is not set (?)
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Input.mousePosition; //gives position where bottom left corner is 0,0
        mousePos.z = 1;

        aimingposition = (mousePos - Camera.main.WorldToScreenPoint(transform.position)).normalized * 0.7f;
        aimingposition.z = 0;

        //      if (Input.GetKeyDown("space"))
        //     {
        //         Roll();
        //     }
        //     if (Input.GetKeyDown(KeyCode.LeftShift))
        //       {
        //         Dash();
        //      }

    }

    public void Grapple(Vector3 grapplePosition)
    {
        this.grapplePosition = grapplePosition;

        grapple = true;


    }
}
