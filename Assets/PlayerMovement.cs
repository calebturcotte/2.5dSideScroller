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
 
    }
    public bool moveRight;
    public bool moveLeft;
    public bool grapple;
    public bool shoot;


    public Animator animator; //var represents the animator object and controls it
   
    public float grappleSpeed = 7f;
    public Vector3 grapplePosition;
    public Rigidbody rb;
    public Camera cam;

    //    private bool dash;
    //  private float dashtime;
    //  private float dashtimecount = 0.15f;
    //   private float dashsize = 1.5f;




    //   private float health = 100f;


    //    private bool damagetaken = false;



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
       
        //     movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        //currently gives position of camera because z is not set (?)
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Input.mousePosition; //gives position where bottom left corner is 0,0
        mousePos.z = 1;

        //      if (Input.GetKeyDown("space"))
        //     {
        //         Roll();
        //     }
        //     if (Input.GetKeyDown(KeyCode.LeftShift))
        //       {
        //         Dash();
        //      }

    }

 //   void FixedUpdate()
  //  {
  //      //rb.AddForce(new Vector3(0, -100, 0));
 //       if (damagetaken)
  //      {
  //          timetrack += Time.deltaTime;
  //          if (damagedTime > timetrack)
 //           {
 //               rb.velocity = -(damagedPosition - rb.position) * 4f;
 //           }
 //           else
 //           {
  //              damagetaken = false;
    //            timetrack = 0;
      //      }
            
 //       }
  //      if (grappling)
    //    {
            //rb.transform.position = Vector2.MoveTowards(rb.position, grapplePosition, 1);
    //        rb.velocity = (grapplePosition - rb.position)*grappleSpeed;
      //      if (Vector2.Distance(rb.transform.position, grapplePosition) < 0.5)
        //    {
                //rb.velocity = Vector3.zero;
          //      rb.velocity = movement * moveSpeed;
            //    grappling = false;
  //              GameObject shooting = FindObjectOfType<Shooting>().gameObject;
      //          shooting.GetComponent<Shooting>().GrappleEnd();
               //rope.positionCount = 0;
    //        }
      //  }
 //       if (dash)
   //     {
     //       dashtime += Time.deltaTime;
       //     if(dashtime < dashtimecount)
         //   {
           //     rb.AddForce(rollmovement, ForceMode.Impulse); //adds a force to rigid body
           // }
     //       else
    //        {
                //rb.AddForce(rb.position, ForceMode2D.Impulse); //adds a force to rigid body
                //rb.velocity = Vector3.zero;
 //               rb.velocity = movement * moveSpeed;
  //              dashtime = 0;
  //              dash = false;
  //          }
            
            //rb.AddForce(rollmovement * moveSpeed, ForceMode2D.Impulse); //adds a force to rigid body
    //    }
  //      else
   //     {
            //rb.AddForce(movement, ForceMode2D.Impulse);
  //          rb.velocity = movement * moveSpeed;
   //     }
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // overides and sets our position

       

 //   }

 //   void Roll()
 //   {
        
  //      rollmovement = mousePos - Camera.main.WorldToScreenPoint(transform.position);
        //rb.velocity = Vector3.zero;
        //vector for our force to be added
   //     rollmovement = new Vector2(rollmovement.x, rollmovement.y).normalized * 3; // remember that mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
     //   dash = true;
       // dashtime = 0;
        //rb.velocity = mousePos*moveSpeed;
    //}

  //  void Dash()
    //{
      //  dashmovement = mousePos - rb.position;

        //normalize our vector then set it to the size we want for our dash size
        //Vector2 dir = new Vector3(dashmovement.x, dashmovement.y).normalized * dashsize;

        //to perform our dash we immediately transform our position to the 
        //transform.position = rb.position + dir;
//        rb.velocity = Vector3.zero;

  //  }

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
}
