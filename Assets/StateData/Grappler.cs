using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Grapple")]
public class Grappler : StateData //by having grapple here, it becomes exclusive to any state able to access "grapple" property
{
    public float moveSpeed;

    public float grappletime;
    public float grapplecooldown;
    public bool grappling;

    public GameObject grapplePrefab;

    private GameObject bullet;

    public float grappleForce = 10f;

    public LineRenderer rope;

    public Player c;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        grappletime = 2f;
        grapplecooldown = 0.7f;
        grappling = false;
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

        c = characterState.GetCharacterControl(animator);

        if (c.grapple)
        {
            //c.grapple = false;
            //animator.SetBool(Player.transitionParameter.shoot.ToString(), false);
            if (!grappling && grappletime > grapplecooldown)
            {
                //grappletime = 0;
                Grapple();
            }

/*            if (grappletime > grapplecooldown && !grappling)
            {
                
                animator.SetBool(Player.transitionParameter.shoot.ToString(), false);
                grappletime = 0;
                grapplecooldown = 0.25f;
                return;
            }*/
            grappletime += Time.deltaTime;


            return;
        }
        else if (!c.grapple)
        {
            animator.SetBool(Player.TransitionParameter.grappling.ToString(), false);
            return;
        }

        void Grapple()
        {
            /*            Vector3 playerposition = c.transform.position;
                        c.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); //translation*/
            if (bullet == null)
            {
                grappletime = 0;
                grappling = true;
                bullet = Instantiate(grapplePrefab, c.aimingposition + c.transform.position, c.transform.rotation); //firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(c.aimingposition * grappleForce, ForceMode.Impulse);
                rope = bullet.GetComponent<LineRenderer>();
                rope.positionCount = 2;
                rope.SetPosition(0, c.transform.position);
                rope.SetPosition(1, bullet.transform.position);
                bullet.GetComponent<Grapple>().SetGrappler(this, c, bullet);
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
