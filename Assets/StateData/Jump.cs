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

 
        PerformJump(characterState, animator);

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public void PerformJump(CharacterState characterState, Animator animator)
    {
        characterState.GetCharacterControl(animator).BiggRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Add force to the rigid body; vector * magnitude of jump
        animator.SetBool(PlayerMovement.TransitionParameter.jump.ToString(), false);
       
    }

}
