using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Idle")]
public class Idle : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Player b = (Player)characterState.GetCharacterControl(animator);
        Debug.Log(b.direction);
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Player c = (Player)characterState.GetCharacterControl(animator);

        if (c.shoot)
        {
            animator.SetBool(Player.TransitionParameter.shoot.ToString(), true);
        }

        if (c.jump)
        {
            animator.SetBool(Player.TransitionParameter.jump.ToString(), true);
        }

        if (c.moveRight) //if input manager's moveRight = true, move
        {
            c.direction = 0;
            animator.SetBool(Player.TransitionParameter.walk.ToString(), true); //moveRight --> turn on movement
        }

        if (c.moveLeft) //if input manager's moveRight = true, move
        {
            c.direction = 1;//set direction to LEFT
            animator.SetBool(Player.TransitionParameter.walk.ToString(), true); //moveLeft --> turn on movement
        }

        if (c.dash)
        {
            animator.SetBool(Player.TransitionParameter.dash.ToString(), true);
        }

        animator.SetBool(Player.TransitionParameter.grappling.ToString(), c.grapple);



    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
