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
      
        PlayerMovement c = characterState.GetCharacterControl(animator);

 
        jump(characterState, animator);

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public void jump(CharacterState characterState, Animator animator)
    {
        Debug.Log(Input.GetMouseButtonDown(0));
        characterState.GetCharacterControl(animator).BiggRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Add force to the rigid body; vector * magnitude of jump
        animator.SetBool(PlayerMovement.transitionParameter.jump.ToString(), false);
       
    }

}
