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

    public enum transitionParameter
    {
        walk,
        shoot,
        jump,
        jumpTransition,
        jumpLanding,
        grounded,
        grappling,
    }
    public bool moveRight;
    public bool moveLeft;
    public bool grapple;
    public bool grappling;
    public bool shoot;
    public bool jump;
    public bool grounded;
    


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

    //Take Damage to our player and give them a slight knockback to movement
    //    public void TakeDamage(float damageamount, Vector3 position)
    //   {
    //       damagetaken = true;
    //        health -= damageamount;
    //        Debug.Log(health);

    //        this.damagedPosition = position;
    //
    //        if(health < 0)
    //        {
    //            Debug.Log("Game Over!");
    //       }
    //    }


    private Rigidbody jRB;
    public Rigidbody BiggRigid
    {
        get
        {
            if (jRB == null)
            {
                jRB = GetComponent<Rigidbody>();
            }
            return jRB;
        }
    }

    public GameObject ColliderEdgePrefab;
    public List<GameObject> BottomSpheres;
    private void Awake()
    {
        BoxCollider box = GetComponent<BoxCollider>(); //get component of the box collider

        float bottom = box.bounds.center.y - box.bounds.extents.y; //defintes the length from center to bottom of box
        float top = box.bounds.center.y + box.bounds.extents.y;//defintes the length from center to top of box
        float front = box.bounds.center.x + box.bounds.extents.x;//defintes the length from center to front (x) of box
        float back = box.bounds.center.x - box.bounds.extents.x;//defintes the length from center to back (x) of box

        GameObject bottomFront = CreateEdgeSphere(new Vector3(front, bottom, 0f));
        GameObject bottomBack = CreateEdgeSphere(new Vector3(back, bottom, 0f));

        bottomFront.transform.parent = this.transform;
        bottomBack.transform.parent = this.transform;

        BottomSpheres.Add(bottomFront);
        BottomSpheres.Add(bottomBack);
    }

    public GameObject CreateEdgeSphere(Vector3 pos)
    {
        GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);
        return obj;
    }

}
