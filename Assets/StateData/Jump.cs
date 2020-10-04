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

 
        PerformJump(characterState, animator, stateInfo);

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(PlayerMovement.TransitionParameter.jump.ToString(), false);
    }

    public void PerformJump(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        /*characterState.GetCharacterControl(animator).BiggRigid.velocity = Vector3.up * jumpForce; //Add force to the rigid body; vector * magnitude of jump*/
        characterState.GetCharacterControl(animator).BiggRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Add force to the rigid body; vector * magnitude of jump
        characterState.GetCharacterControl(animator).jump = false;
    }

}
