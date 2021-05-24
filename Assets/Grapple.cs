using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private Player player;
    private GameObject hook;
    private Grappler grappler;
    private float grappleSpeed = 7f;
    private float currentRopeLength;
    private bool hooked;

    FixedJoint joint;
    public LineRenderer rope;
    public Rigidbody tempRB;

    void OnCollisionEnter(Collision collision)
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
            }
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
        hooked = false;
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
        currentRopeLength = Vector3.Distance(playerPosition, shotPosition);

        if (player.grappling) //if grappling is true
        {
            if(currentRopeLength >= grappler.grappleLength)
            {
                EndHook();
            }

            if(hooked) // if current rope length < grappleLength
            {
                Vector3 hangingPosition = shotPosition + (playerPosition - shotPosition).normalized * 1.5f;
                tempRB = GetComponent<Rigidbody>();
                tempRB.velocity = player.BiggRigid.velocity;

                player.BiggRigid.velocity = Vector3.zero;
                player.BiggRigid.transform.position = Vector3.Lerp(playerPosition,hangingPosition, Time.deltaTime * grappleSpeed);
                CheckRigidbodyProperty(player.BiggRigid, tempRB, playerPosition, hangingPosition);
            } 
        } else
        {
            EndHook();
        }
    }

    public void EndHook()
    {
        player.BiggRigid.useGravity = true;
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

    public Rigidbody CheckRigidbodyProperty(Rigidbody rb, Rigidbody tempRB, Vector3 playerPosition, Vector3 hangingPosition)
    {
        if (currentRopeLength >= 3.5f) // arbitrary length of rope
        {
            rb.velocity = Vector3.zero;
        }
        else if (currentRopeLength < 3.5f) // arbitrary length of rope
        {
            rb.velocity = tempRB.velocity;
            rb.AddForce((hangingPosition - playerPosition) * 0.2f, ForceMode.Impulse);
        }
        return rb;
    }
}
