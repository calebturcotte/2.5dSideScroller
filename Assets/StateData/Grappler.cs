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

    public PlayerMovement c;

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
            //animator.SetBool(PlayerMovement.transitionParameter.shoot.ToString(), false);
            if (!grappling && grappletime > grapplecooldown)
            {
                //grappletime = 0;
                Grapple();
            }

/*            if (grappletime > grapplecooldown && !grappling)
            {
                
                animator.SetBool(PlayerMovement.transitionParameter.shoot.ToString(), false);
                grappletime = 0;
                grapplecooldown = 0.25f;
                return;
            }*/
            grappletime += Time.deltaTime;


            return;
        }
        else if (!c.grapple)
        {
            animator.SetBool(PlayerMovement.transitionParameter.grappling.ToString(), false);
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

            //Destroy(bullet, 5f);
            //animator.SetBool(PlayerMovement.transitionParameter.grappling.ToString(), false);
            //c.grapple = false;
        }
        /*        if (c.grapple)
                {
                    c.rb.transform.position = Vector2.MoveTowards(c.rb.position, c.grapplePosition, 1);
                    c.rb.velocity = (c.grapplePosition - c.rb.position) * c.grappleSpeed;
                    if (Vector2.Distance(c.rb.transform.position, c.grapplePosition) < 0.5)
                    {
                        c.rb.velocity = Vector3.zero;
                        c.rb.velocity = c.movement * moveSpeed;
                        c.grapple = false;

                        GameObject shooting = FindObjectOfType<Shooting>().gameObject;
                        shooting.GetComponent<Shooting>().GrappleEnd();
                        rope.positionCount = 0;
                    }

                }*/
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
