using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    private GameObject hook;

    //public Rigidbody hookrigidbody;

    private Grappler grappler;

    private bool returning;

    private Player player;

    public float currentRopeLength;

    private bool grappling;
    private float grappleSpeed = 5f;

    //private float grappletime = 0.5f;
    //private float grappletimecount;

    private bool hooked;
    private bool hanging;
    FixedJoint joint;

    public LineRenderer rope;

    private Transform ropeTarget;


    void OnCollisionEnter(Collision collision)
    {
        if (!returning) //not returning
        {
            hooked = true;
            if(joint == null)
            {
                //For now any moving platforms and such need a rigidbody
                joint = gameObject.AddComponent<FixedJoint>();
                joint.anchor = collision.contacts[0].point;
                // conects the joint to the other object
                joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
                // Stops objects from continuing to collide and creating more joints
                joint.enableCollision = false;

                if (collision.gameObject.CompareTag("MovingPlatform")) // if the collision is with a platform and you're ON TOP
                {
                    transform.SetParent(collision.gameObject.transform);
                }
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
        
        EndHook();
        Destroy(gameObject);
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
        //grappletimecount = 0f;
        returning = false;
        hooked = false;
        hanging = false;


    }


    public void Start()
    {
        rope = GetComponent<LineRenderer>();
        rope.positionCount = 2;

    }

    void Update()
    {
        DrawRope();

        Vector3 playerPosition = player.transform.position;
        Vector3 shotPosition = hook.transform.position;
        Vector3 hangingPosition = shotPosition + (playerPosition - shotPosition).normalized * 1.5f;



        currentRopeLength = (player.transform.position - hook.transform.position).magnitude;


        if (player.grappling) //if grappling is true
        {
            if(currentRopeLength >= grappler.grappleLength)
            {
                EndHook();
            }

            if(hooked) // if current rope length < grappleLength
            {
                player.BiggRigid.velocity = Vector3.zero;
                player.BiggRigid.transform.position = Vector3.Lerp(playerPosition,hangingPosition, Time.deltaTime * grappleSpeed);
                
                if(!player.grounded)
                {
                    hanging = true; //hanging may be unnecessary if not grounded always means hanging in this context
                } 
                
            } 
        } else
        {
            EndHook();
        }

        /*if (hooked)
        {
            if (player.grappling)
            {
                
                if (Vector2.Distance(playerPosition, shotPosition) > grappler.grappleLength)
                {

                    player.BiggRigid.transform.position = shotPosition + (playerPosition - shotPosition).normalized * 1.5f;
                    rope.SetPosition(0, grappler.c.transform.position);
                    rope.SetPosition(1, transform.position);
                    if ((shotPosition.y - playerPosition.y) >= 1.54)
                    {
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
            player.BiggRigid.velocity = (shotPosition - playerPosition) * grappleSpeed;
            if (Vector2.Distance(playerPosition, shotPosition) < 1)
            {
                hooked = true;
            }
        }
        else if (grappletimecount > grappletime && !returning)
        {
            returning = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); 
            GetComponent<Rigidbody>().AddForce((playerPosition - shotPosition) *grappleSpeed, ForceMode.Impulse);
            grappletime = 0;
        }
        else if (returning && Vector2.Distance(playerPosition, shotPosition) < 2)
        {
            EndHook();
        }
        grappletimecount += Time.deltaTime;*/
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
        rope.positionCount = 0;
        Destroy(hook);
    }

    public void DrawRope()
    {

        rope.SetPosition(0, player.transform.position); //rope starts at player's position
        rope.SetPosition(1, transform.position); //rope ends at grappleShot's position
    }
}
