using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public GameObject platform;
    public Rigidbody rb;

    private Vector3 movement;
    private float angle = 0;
    private float monsterdamage = 20f;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 lookDir = transform.position - platform.transform.position;

        angle =Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //split our movement into 4 chunks based on where the ball is currently
        if (angle < 0 && angle > -90)
        {
            rb.velocity = new Vector3(2f, -2f, 0);
        }
        else if (angle < 0 && angle > -180)
        {
            rb.velocity = new Vector3(-2f, -2f, 0);
        }
        else if (angle < 0 && angle > -270)
        {
            rb.velocity = new Vector3(-2f, 2f, 0);
        }
        else
        {
            rb.velocity = new Vector3(2f, 2f, 0);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
  //      GameObject player = FindObjectOfType<PlayerMovement>().gameObject;

        //move our game object to collision point
   //     if(collision.gameObject.Equals(player))
   //     player.GetComponent<PlayerMovement>().TakeDamage(monsterdamage, rb.transform.position); //grab position of the rigidbody attached to the grapple
    }
}
