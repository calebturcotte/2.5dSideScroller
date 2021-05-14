using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/JumpLanding")]
public class JumpLanding : StateData
{
 
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Character c = characterState.GetCharacterControl(animator);
        animator.SetBool(Player.TransitionParameter.jumpLanding.ToString(), true);
        animator.SetBool(Player.TransitionParameter.jumpTransition.ToString(), false);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {


    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {


    }


}
