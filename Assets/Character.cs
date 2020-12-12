﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /**
     * Our Handler for generic movement
     */
    //public InventoryObject inventory;
    //[SerializeField] public LayerMask platformLayerMask;
    public int health;
    public enum TransitionParameter
    {
        walk,
        shoot,
        jump,
        jumpTransition,
        jumpLanding,
        grounded,
        grappling,
        dash,
        colliding,
    }
    public bool moveRight;
    public bool moveLeft;
    public bool grapple;
    public bool grappling;
    public bool shoot;
    public bool jump;
    public bool grounded;
    public bool dash;
    public bool getItem;


    public int direction = 0; //you always start off facing RIGHT

    public Animator animator; //var represents the animator object and controls it

    private Rigidbody rb;
    public Camera cam;

    public Vector3 aimingposition;
    public Vector3 movement;


    public virtual void Update()
    {

        //Collision Detection
        if (isColliding()) //if the player is colliding
        {
            if (!tag.Equals("Collectible"))
            {
                animator.SetBool(TransitionParameter.colliding.ToString(), true); //display TRUE
                animator.SetBool(TransitionParameter.walk.ToString(), false); //turn of walking (prevents clipping) note: will start a cycle between idle and walk states     
            }
            else
            {
                animator.SetBool(Character.TransitionParameter.colliding.ToString(), false);
            }

        }
        else
        {
            animator.SetBool(Character.TransitionParameter.colliding.ToString(), false);
        }

        //Ground Detection
        if (IsGrounded()) //if the player is touching the ground
        {

            animator.SetBool(Character.TransitionParameter.grounded.ToString(), true);
        }
        else
        {
            animator.SetBool(Character.TransitionParameter.grounded.ToString(), false);
        }


    }

    public Rigidbody BiggRigid
    {
        get
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            return rb;
        }
    }


    private void Awake()
    {



    }


    public virtual void OnTriggerEnter(Collider other)
    {

    }


    public virtual void OnApplicationQuit()
    {
        
    }

    public bool isColliding()  //collision detection needs to be on a different layer from items
    {
        BoxCollider box = GetComponent<BoxCollider>(); //get component of the box collider
        LayerMask myMask = LayerMask.GetMask("TransparentFX");// transparent layer we want to avoid stopping for, add other Strings inside brackets for other layers we may need to ignore
        Vector3 correction = new Vector3(0, (transform.localScale.y / 2), 0);
        int directionModifier;

        if (direction == 0) // if you are facing RIGHT
        {
            directionModifier = 1;
        }
        else // the only other possible value is direction = 1; facing LEFT
        {
            directionModifier = -1;
        }

        bool boxCastHit = Physics.BoxCast(box.bounds.center + (Vector3.left * 0.1f * directionModifier), transform.localScale / 2 + correction, Vector3.right * directionModifier, transform.rotation, 0.1f, ~myMask); //seems to be the most precise way to do this so far
        return boxCastHit; //return the value of boxCastHit
    }


    public bool IsGrounded() //Ground collision checker
    {
        RaycastHit m_Hit;
        Vector3 correction = new Vector3((transform.localScale.x / 2), 0, 0);
        BoxCollider box = GetComponent<BoxCollider>(); //get component of the box collider
        bool boxCastHit = Physics.BoxCast(box.bounds.center + (Vector3.up * 0.1f), transform.localScale / 2 + correction, Vector3.down, out m_Hit, transform.rotation, transform.localScale.y / 4);
        if (!boxCastHit)
        {
            transform.parent = null;
        }
        else
        {
            transform.parent = m_Hit.collider.transform;
        }
        //add 0.1f to initial boxcast. IF we just use the center, the boxcast will start as intersecting = FALSE
        return boxCastHit; //if raycast does not touch anything within this distance, player is NOT grounded
    }


}
