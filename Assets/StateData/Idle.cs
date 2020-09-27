using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Idle")]
public class Idle : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
     
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        PlayerMovement c = characterState.GetCharacterControl(animator);

        if (c.shoot)
        {
            animator.SetBool(PlayerMovement.transitionParameter.shoot.ToString(), true);
        }

        if (c.moveRight) //if input manager's moveRight = true, move
        {
   
            animator.SetBool(PlayerMovement.transitionParameter.walk.ToString(), true); //moveRight --> turn on movement
        }

        if (c.moveLeft) //if input manager's moveRight = true, move
        {

            animator.SetBool(PlayerMovement.transitionParameter.walk.ToString(), true); //moveLeft --> turn on movement
        }

        /*        if (c.grapple)
                {
                    animator.SetBool(PlayerMovement.transitionParameter.grappling.ToString(), true);
                }*/
        animator.SetBool(PlayerMovement.transitionParameter.grappling.ToString(), c.grapple);

        if (c.jump)
        {
            animator.SetBool(PlayerMovement.transitionParameter.jump.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
