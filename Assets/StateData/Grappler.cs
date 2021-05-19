using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Grapple")]
public class Grappler : StateData //by having grapple here, it becomes exclusive to any state able to access "grapple" property
{
    public float moveSpeed;

    public float grappleTime;
    public float grappleCooldown;
    public bool grappling;
    public float grappleLength;

    public GameObject grapplePrefab;

    public GameObject grappleShot;

    public float grappleForce = 10f;



    public Player c;


    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        grappleTime = 2f;
        grappleCooldown = 0.7f;
        grappling = false;
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

        c = (Player) characterState.GetCharacterControl(animator);

        if (c.grapple)
        {
            //c.grapple = false;
            //animator.SetBool(Player.transitionParameter.shoot.ToString(), false);
            if (!grappling && grappleTime >= grappleCooldown)
            {
                Grapple();
            }
            grappleTime += Time.deltaTime;            
        }
        else if (!c.grapple)
        {
            animator.SetBool(Player.TransitionParameter.grappling.ToString(), false);
        }

        
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    void Grapple()
    {
        if (grappleShot == null)
        {
            grappleShot = Instantiate(grapplePrefab, c.aimingposition + c.transform.position, c.transform.rotation); //firePoint.position, firePoint.rotation);               
            Rigidbody rb = grappleShot.GetComponent<Rigidbody>();
            rb.AddForce(c.aimingposition * grappleForce, ForceMode.Impulse);
            grappleTime = 0;

            //if (GrappleCheck()) //raycast returns true, otherwise don't toggle grappling state
            //{
                grappling = true;
                grappleShot.GetComponent<Grapple>().SetGrappler(this, c, grappleShot); // fixes to be made here
            //}
        }
    }

    public bool GrappleCheck()
    {
        LayerMask ignore = LayerMask.GetMask("Player");
        BoxCollider box = c.GetComponent<BoxCollider>(); //get component of the box collider
        bool grapple = Physics.Raycast(box.bounds.center, c.aimingposition, grappleLength, ~ignore);
        return grapple;

    }

}
