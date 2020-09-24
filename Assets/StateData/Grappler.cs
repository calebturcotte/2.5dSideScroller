using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Grapple")]
public class Grappler : StateData //by having grapple here, it becomes exclusive to any state able to access "grapple" property
{
    public float moveSpeed;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    // void Grapple()
    //{
    //  grapple = Instantiate(grapplePrefab, firePoint.position, firePoint.rotation);
    //grapplerb = grapple.GetComponent<Rigidbody>();
    //        grapplerb.AddForce(firePoint.up * grappleForce, ForceMode.Impulse);
    //  }
    // Update is called once per frame
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    //    PlayerMovement c = characterState.GetCharacterControl(animator);
        

       // if (c.grapple)
       // {
         //   c.rb.transform.position = Vector2.MoveTowards(c.rb.position, c.grapplePosition, 1);
           // c.rb.velocity = (c.grapplePosition - c.rb.position) * c.grappleSpeed;
//            if (Vector2.Distance(c.rb.transform.position, c.grapplePosition) < 0.5)
  //          {
    //            c.rb.velocity = Vector3.zero;
      //          c.rb.velocity = c.movement * moveSpeed;
        //        c.grapple = false;

          //      GameObject shooting = FindObjectOfType<Shooting>().gameObject;
            //    shooting.GetComponent<Shooting>().GrappleEnd();
                //rope.positionCount = 0;
            //}

       // }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
