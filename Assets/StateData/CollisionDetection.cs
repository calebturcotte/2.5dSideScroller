using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/CollisionDetection")]
public class CollisionDetection : StateData
{
    

    public float distance;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        PlayerMovement c = characterState.GetCharacterControl(animator);
        if (c.isColliding(c))
        {
            animator.SetBool(PlayerMovement.TransitionParameter.colliding.ToString(), true);
            //c.BiggRigid.velocity = Vector3.zero * 0;
            animator.SetBool(PlayerMovement.TransitionParameter.walk.ToString(), false);
            animator.SetBool(PlayerMovement.TransitionParameter.dash.ToString(), false);
        }
        else
        {
            animator.SetBool(PlayerMovement.TransitionParameter.colliding.ToString(), false);
        }

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }






}
