using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /**
     * Our Handler for generic movement
     */
    //public InventoryObject inventory;
    //[SerializeField] public LayerMask platformLayerMask;
    public int characterMaxHealth;
    public int currentHealth;
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

    public int direction = 1; //you always start off facing RIGHT

    public Animator animator; //var represents the animator object and controls it

    private Rigidbody rb;

    public Vector3 aimingposition;
    public Vector3 movement;


    public virtual void Awake()
    {

        currentHealth = characterMaxHealth; //set current health to max

    }
    public virtual void Update()
    {


    }

    public virtual void FixedUpdate()
    {
        //Collision Detection
        if (IsColliding()) //while the character is colliding
        {
            //this aint ever accessed anyways, isColliding ALWAYS returns false
            animator.SetBool(TransitionParameter.colliding.ToString(), true); //colliding is TRUE
            animator.SetBool(TransitionParameter.walk.ToString(), false); //turn of walking (prevents clipping) note: will start a cycle between idle and walk states     

        }
        else
        {
            animator.SetBool(Character.TransitionParameter.colliding.ToString(), false);

        }

        //Ground Detection
        if (IsGrounded()) //if the player is touching the ground
        {

            animator.SetBool(Character.TransitionParameter.grounded.ToString(), true);
            //Debug.Log("Grounded!");
        }
        else
        {
            animator.SetBool(Character.TransitionParameter.grounded.ToString(), false);
            //Debug.Log("Not");
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            BiggRigid.velocity = Vector3.zero;
            //animator.SetBool(TransitionParameter.walk.ToString(), false); //turn of walking (prevents clipping) note: will start a cycle between idle and walk states     
        }
    }

    public virtual void OnCollisionStay(Collision collision)
    {
       /* if (collision.collider.CompareTag("Platform") && !isColliding())
        {
            Debug.Log("engage");
            BiggRigid.velocity = Vector3.zero;
            //animator.SetBool(TransitionParameter.walk.ToString(), false); //turn of walking (prevents clipping) note: will start a cycle between idle and walk states     
        }*/
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


    public virtual void OnApplicationQuit()
    {
        
    }

    public virtual void DamageTaken(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual bool IsColliding()  //collision detection needs to be on a different layer from items
    {
        BoxCollider box = GetComponent<BoxCollider>(); //get component of the box collider
        LayerMask myMask = LayerMask.GetMask("TransparentFX");// transparent layer we want to avoid stopping for, add other Strings inside brackets for other layers we may need to ignore
        Vector3 correction = new Vector3(0, 0.1f, 0);
        bool boxCastHit = Physics.BoxCast(box.bounds.center, box.bounds.extents - correction, Vector3.right * direction, Quaternion.identity, 0.1f, ~myMask); //seems to be the most precise way to do this so far


        return boxCastHit; //return the value of boxCastHit
    }

    public bool IsGrounded() //Ground collision checker
    {


        BoxCollider box = GetComponent<BoxCollider>(); //get component of the box collider
        bool boxCastHit = Physics.BoxCast(box.bounds.center + (Vector3.up * 0.1f), transform.localScale/2, Vector3.down, Quaternion.identity, transform.localScale.y/4);

        //Debug.DrawRay(box.bounds.center + (Vector3.up * 0.1f), Vector3.down * (transform.localScale.y/4), Color.red);
        //Debug.Log(boxCastHit);
        //add 0.1f to initial boxcast. IF we just use the center, the boxcast will start as intersecting with itself = FALSE
        return boxCastHit; //if raycast does not touch anything within this distance, player is NOT grounded
    }


}
