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

        if (IsGrounded(c))
        {
            animator.SetBool(PlayerMovement.TransitionParameter.grounded.ToString(), true);

        }
        else
        {
            animator.SetBool(PlayerMovement.TransitionParameter.grounded.ToString(), false);

        }

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }


    bool IsGrounded(PlayerMovement c)
    {

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
