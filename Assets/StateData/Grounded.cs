using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Grounded")]
public class Grounded : StateData
{

    public float distance;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

 

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        PlayerMovement c = characterState.GetCharacterControl(animator);

        if (grounded(c))
        {
            animator.SetBool(PlayerMovement.transitionParameter.jump.ToString(), false);

        }

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }


    bool grounded(PlayerMovement c)
    {
        if (c.BiggRigid.velocity.y <= 0f && c.BiggRigid.velocity.y > -0.01f) //if y velocity is greater than -0.01 and less than 0, grounded
        {
            return true;
        }

        foreach (GameObject o in c.BottomSpheres) //checks collsion for each sphere in this list
        {
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, -Vector3.up, out hit, distance)) //raycast for a certain length
            {
                return true; //if raycast touches something, player is grounded
            }
        }
        return false; //if raycast does not touch anything within this distance, player is NOT grounded
    }



}
