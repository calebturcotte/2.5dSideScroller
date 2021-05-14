using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Jump")]
public class Jump : StateData
{

    public float jumpForce;


    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Character c = characterState.GetCharacterControl(animator); //for jumping upon entering the state

        if (!c.IsGrounded())
        {
            animator.SetBool(Player.TransitionParameter.jump.ToString(), false);
        } else
        {
            PerformJump(characterState, animator, stateInfo);
            animator.SetBool(Player.TransitionParameter.jump.ToString(), false);
        }
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Character c = characterState.GetCharacterControl(animator);
        if (!c.IsGrounded()) //if grounded BECOMES true during any aerial frame
        {
            animator.SetBool(Player.TransitionParameter.jump.ToString(), false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public void PerformJump(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Character c = characterState.GetCharacterControl(animator);
        c.BiggRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Add force to the rigid body; vector * magnitude of jump
    }

}

