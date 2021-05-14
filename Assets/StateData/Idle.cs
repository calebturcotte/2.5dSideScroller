using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Idle")]
public class Idle : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
/*        Player b = (Player)characterState.GetCharacterControl(animator);
        Debug.Log(b.direction);*/
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Character c = (Character)characterState.GetCharacterControl(animator);

        if (c.shoot)
        {
            animator.SetBool(Character.TransitionParameter.shoot.ToString(), true);
        }

        if (c.jump)
        {
            animator.SetBool(Character.TransitionParameter.jump.ToString(), true);
        }

        if (c.moveRight) //if input manager's moveRight = true, move
        {
            c.direction = 1; //set direction to RIGHT
            animator.SetBool(Character.TransitionParameter.walk.ToString(), true); //moveRight --> turn on movement
        }

        if (c.moveLeft) //if input manager's moveRight = true, move
        {
            c.direction = -1; //set direction to LEFT
            animator.SetBool(Character.TransitionParameter.walk.ToString(), true); //moveLeft --> turn on movement
        }

        if (c.dash)
        {
            animator.SetBool(Character.TransitionParameter.dash.ToString(), true);
        }

        animator.SetBool(Character.TransitionParameter.grappling.ToString(), c.grapple);



    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
