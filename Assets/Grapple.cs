using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    private GameObject hook;

    public Rigidbody hookrigidbody;

    private Grappler grappler;

    private bool returning;

    private Player c;

    private bool grappling;
    private readonly float grappleSpeed = 7f;

    private float grappletime = 0.5f;
    private float grappletimecount;

    private bool hooked;
    private bool hanging;
    FixedJoint joint;


    void OnCollisionEnter(Collision collision)
    {
        if (!returning) //not returning
        {
            grappling = true;
            /*            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;*/

            if(joint == null)
            {
                joint = gameObject.AddComponent<FixedJoint>();
                joint.anchor = collision.contacts[0].point;
                // conects the joint to the other object
                joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
                // Stops objects from continuing to collide and creating more joints
                joint.enableCollision = false;
            }
        }
        else
        {
            EndHook();
        }
    }

    //remove our object once it leaves the screen
    void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }

    public void SetGrappler(Grappler grappler, Player c, GameObject hook)
    {
        if(this.c == null)
        {
            this.c = c;
        }
        this.hook = hook;
        this.grappler = grappler;
        grappletimecount = 0f;
        returning = false;
        hooked = false;
        hanging = false;
        
    }

    void Update()
    {  

        grappler.rope.positionCount = 2;
        grappler.rope.SetPosition(0, grappler.c.transform.position);
        grappler.rope.SetPosition(1, transform.position);
        Vector2 tempposition = c.transform.position;
        Vector2 tempposition2 = hook.transform.position;

        if (hooked)
        {
            if (c.grappling)
            {
                
                if (Vector2.Distance(tempposition, tempposition2) > 1.5)
                {

                    //c.rb.velocity = new Vector3(0,0,0);
                    c.BiggRigid.transform.position = tempposition2 + (tempposition - tempposition2).normalized * 1.5f;
                    grappler.rope.SetPosition(0, grappler.c.transform.position);
                    grappler.rope.SetPosition(1, transform.position);
                    if ((tempposition2.y-tempposition.y) >= 1.54)
                    {
                        //c.rb.useGravity = false;
                        c.BiggRigid.velocity = new Vector3(0, 0, 0);
                        hanging = true;

                    }
                    else
                    {
                        //c.rb.useGravity = true;
                    }
                }
            }
            else
            {
                EndHook();
            }
        }
        else if (grappling && !returning)
        {
            c.BiggRigid.velocity = (tempposition2 - tempposition) * grappleSpeed;
            if (Vector2.Distance(tempposition,tempposition2) < 1)
            {
                hooked = true;
            }
        }
        else if (grappletimecount > grappletime && !returning)
        {
            returning = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); 
            GetComponent<Rigidbody>().AddForce((tempposition-tempposition2)*grappleSpeed, ForceMode.Impulse);
            grappletime = 0;
        }
        else if (returning && Vector2.Distance(tempposition, tempposition2) < 2)
        {
            EndHook();
        }
        grappletimecount += Time.deltaTime;
    }

    void EndHook()
    {
        c.BiggRigid.useGravity = true;
        if (hanging)
        {
            c.BiggRigid.velocity = new Vector3(0, 0, 0);
            //hooked = false;
        }
        c.grapple = false;
        grappler.grappling = false;
        grappler.rope.positionCount = 0;
        Destroy(hook);
    }
}
