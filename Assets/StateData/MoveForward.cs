using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/MoveForward")]
public class MoveForward : StateData
{

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        PlayerMovement c = characterState.GetCharacterControl(animator);
        
        if (c.moveRight && c.moveLeft)
        {
            animator.SetBool(PlayerMovement.TransitionParameter.walk.ToString(), false);
            return;
        }

        if (!c.moveRight && !c.moveLeft)
        {
            animator.SetBool(PlayerMovement.TransitionParameter.walk.ToString(), false);
            return;
        }

        if (c.moveRight) //if input manager's moveRight = true, move
        {
            c.transform.Translate(Vector3.right *c.movespeed * Time.deltaTime); //translation
            c.transform.rotation = Quaternion.Euler(0f, 0f, 0f); //RIGHT = forward, positive direction

        }

        if (c.moveLeft) //if input manager's moveLeft = true, move
        {
            c.transform.Translate(Vector3.right * c.movespeed * Time.deltaTime);
            c.transform.rotation = Quaternion.Euler(0f, 180f, 0f); //LEFT = backward, negative direction

        }

        if (c.jump)
        {
            animator.SetBool(PlayerMovement.TransitionParameter.jump.ToString(), true);
        }
       
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }


}
