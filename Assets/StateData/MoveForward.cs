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
        
        if (c.moveRight && c.moveLeft) //if both inputs are detected
        {
            animator.SetBool(PlayerMovement.TransitionParameter.walk.ToString(), false);
            return;
        }

        if (!c.moveRight && !c.moveLeft) //if no input detected (may not be necessary)
        {
            animator.SetBool(PlayerMovement.TransitionParameter.walk.ToString(), false);
            return;
        }

        if (c.isColliding(c))
        {
            animator.SetBool(PlayerMovement.TransitionParameter.colliding.ToString(), true);
            //c.BiggRigid.velocity = Vector3.zero * 0;
            animator.SetBool(PlayerMovement.TransitionParameter.walk.ToString(), false);
            animator.SetBool(PlayerMovement.TransitionParameter.dash.ToString(), false);
        }
        else
        {
            animator.SetBool(PlayerMovement.TransitionParameter.colliding.ToString(), false);
            if (c.moveRight) //if input manager's moveRight = true, move
            {
                c.transform.Translate(Vector3.right * c.movespeed * Time.deltaTime); //translation                                                                         
                c.transform.rotation = Quaternion.Euler(0f, 0f, 0f); //object rotates to FACE RIGHT = forward, positive direction
                //c.BiggRigid.velocity = new Vector3(c.movespeed, c.BiggRigid.velocity.y, 0);                             
            }

            if (c.moveLeft) //if input manager's moveLeft = true, move
            {

                c.transform.Translate(Vector3.right * c.movespeed * Time.deltaTime);
                c.transform.rotation = Quaternion.Euler(0f, 180f, 0f); //object rotates to FACE LEFT (more visible with models, not cube) = backward, negative direction
                //c.BiggRigid.velocity = new Vector3(-c.movespeed, c.BiggRigid.velocity.y, 0);

            }

        }

        if (c.dash)
        {
            animator.SetBool(PlayerMovement.TransitionParameter.dash.ToString(), true);
        }


    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {



    }


}
