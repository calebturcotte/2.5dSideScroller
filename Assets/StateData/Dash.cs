using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Dash")]
public class Dash : StateData
{
    public float dashSpeed;
    private float dashTime;
    public float dashCooldown; //0.6 is smooth, 0.7 is middle ground, 0.8 is punishing
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Player c = characterState.GetCharacterControl(animator);

        dashTime = 0f;

        if (c.direction == 0)
        {
            characterState.GetCharacterControl(animator).BiggRigid.AddForce(Vector3.right * dashSpeed, ForceMode.Impulse);

        }
        else if (c.direction == 1)
        {
            characterState.GetCharacterControl(animator).BiggRigid.AddForce(-Vector3.right * dashSpeed, ForceMode.Impulse);
        }
            


    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        dashTime += Time.deltaTime; //iterate on each update
        if (dashTime > dashCooldown) { //when dashtime reaches/passes dashCooldown, turn it to false. You are HOLDING the state to "cooldown"
        animator.SetBool(Player.TransitionParameter.dash.ToString(), false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        
    }


}
