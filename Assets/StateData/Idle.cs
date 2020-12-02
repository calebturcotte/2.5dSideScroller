using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Idle")]
public class Idle : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        PlayerMovement b = (PlayerMovement)characterState.GetCharacterControl(animator);
        Debug.Log(b.direction);
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        PlayerMovement c = (PlayerMovement)characterState.GetCharacterControl(animator);

        if (c.shoot)
        {
            animator.SetBool(PlayerMovement.TransitionParameter.shoot.ToString(), true);
        }

        if (c.jump)
        {
            animator.SetBool(PlayerMovement.TransitionParameter.jump.ToString(), true);
        }

        if (c.moveRight) //if input manager's moveRight = true, move
        {
            c.direction = 0;
            animator.SetBool(PlayerMovement.TransitionParameter.walk.ToString(), true); //moveRight --> turn on movement
        }

        if (c.moveLeft) //if input manager's moveRight = true, move
        {
            c.direction = 1;//set direction to LEFT
            animator.SetBool(PlayerMovement.TransitionParameter.walk.ToString(), true); //moveLeft --> turn on movement
        }

        if (c.dash)
        {
            animator.SetBool(PlayerMovement.TransitionParameter.dash.ToString(), true);
        }

        animator.SetBool(PlayerMovement.TransitionParameter.grappling.ToString(), c.grapple);


    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
