using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public GameObject hook;

    public Rigidbody hookrigidbody;
    //private PlayerMovement playermovement;
    
    
    void OnCollisionEnter(Collision collision)
    {
        //tell the grappling hook we hit something
   //     GameObject grappleshot = FindObjectOfType<Shooting>().gameObject;
    //    grappleshot.GetComponent<Shooting>().GrappleHit();

        //get the player object
  //      GameObject player = FindObjectOfType<PlayerMovement>().gameObject;

        //move our game object to collision point
   //     player.GetComponent<PlayerMovement>().Grapple(hook.transform.position); //grab position of the rigidbody attached to the grapple

  //      hookrigidbody.velocity = Vector3.zero;
        
    }

    //remove our object once it leaves the screen
    void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }
}
