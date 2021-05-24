using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/MoveForward")]
public class MoveForward : StateData
{
    public float movespeed;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Character c = characterState.GetCharacterControl(animator); //create an object of the Player Class

        if (c.IsColliding()) //calls on the isColliding method; if collision returns TRUE
        {
            animator.SetBool(Player.TransitionParameter.colliding.ToString(), true); //display TRUE in animator
            animator.SetBool(Player.TransitionParameter.walk.ToString(), false); //end the WALKING state
        }
        else
        {
            animator.SetBool(Player.TransitionParameter.colliding.ToString(), false);

            if (c.moveRight && c.moveLeft) //if both inputs are detected
            {

                animator.SetBool(Player.TransitionParameter.walk.ToString(), false);
                return;
            }

            if (!c.moveRight && !c.moveLeft) //if no input detected (may not be necessary)
            {
                animator.SetBool(Player.TransitionParameter.walk.ToString(), false);
                return;
            }

               
            if (c.moveRight) //if input manager's moveRight = true, move
            {
                c.transform.rotation = Quaternion.Euler(0f, 0f, 0f); //object rotates to FACE RIGHT = forward, positive direction                                                                                                
                c.transform.Translate(Vector3.right * movespeed * Time.deltaTime); //translation                                                                             
            }

              
            if (c.moveLeft) //if input manager's moveLeft = true, move
            {
                c.transform.rotation = Quaternion.Euler(0f, 180f, 0f); //object rotates to FACE LEFT (more visible with models, not cube) = backward, negative direction 
                c.transform.Translate(Vector3.right * movespeed * Time.deltaTime);
            }
        
        
            if (c.dash)
            {
                animator.SetBool(Player.TransitionParameter.dash.ToString(), true);
            }

            if (c.jump)
            {
                animator.SetBool(Character.TransitionParameter.jump.ToString(), true);
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {



    }


}
