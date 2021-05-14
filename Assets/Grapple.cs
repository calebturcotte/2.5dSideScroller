using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    private GameObject hook;

    public Rigidbody hookrigidbody;

    private Grappler grappler;

    private bool returning;

    private Player player;

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
                //For now any moving platforms and such need a rigidbody
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
        if(this.player == null)
        {
            this.player = c;
            c.grappleObject = gameObject;
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
        Vector2 tempposition = player.transform.position;
        Vector2 tempposition2 = hook.transform.position;

        if (hooked)
        {
            if (player.grappling)
            {
                
                if (Vector2.Distance(tempposition, tempposition2) > 1.5)
                {

                    //c.rb.velocity = new Vector3(0,0,0);
                    player.BiggRigid.transform.position = tempposition2 + (tempposition - tempposition2).normalized * 1.5f;
                    grappler.rope.SetPosition(0, grappler.c.transform.position);
                    grappler.rope.SetPosition(1, transform.position);
                    if ((tempposition2.y-tempposition.y) >= 1.54)
                    {
                        //c.rb.useGravity = false;
                        player.BiggRigid.velocity = new Vector3(0, 0, 0);
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
            player.BiggRigid.velocity = (tempposition2 - tempposition) * grappleSpeed;
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

    public void EndHook()
    {
        player.BiggRigid.useGravity = true;
        if (hanging)
        {
            player.BiggRigid.velocity = new Vector3(0, 0, 0);
            //hooked = false;
        }
        player.grapple = false;
        player.grappleObject = null;
        grappler.grappling = false;
        grappler.rope.positionCount = 0;
        Destroy(hook);
    }
}
