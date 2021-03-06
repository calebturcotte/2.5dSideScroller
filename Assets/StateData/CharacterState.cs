﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : StateMachineBehaviour
{
    public List<StateData> ListAbilityData = new List<StateData>(); //create a list of states

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (StateData d in ListAbilityData)
        {
            try
            {
                d.OnEnter(this, animator, stateInfo);
            }
            catch (System.Exception e)
            {
                Debug.LogError("StateData: " + d + "Animator: " + animator + "Statenfo: " + stateInfo);
            }
        }
    }


    public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) //update the list; adds StateData for each n
    {
        foreach(StateData n in ListAbilityData) //goes through each item and updates
        {
            try
            {
                n.UpdateAbility(characterState, animator, stateInfo);
            }
            catch (System.Exception e)
            {
                Debug.LogError("StateData: " + n + "Animator: " + animator + "Statenfo: " + stateInfo);
            }
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        UpdateAll(this, animator, stateInfo); // applies the method above
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (StateData d in ListAbilityData)
        {
            try
            {
                d.OnExit(this, animator, stateInfo);
            }
            catch (System.Exception e)
            {
                Debug.LogError("StateData: " + d + "Animator: " + animator + "Statenfo: " + stateInfo);
            }
        }
    }

    private Character characterControl;
    public Character GetCharacterControl(Animator animator) //making it such that every script can access CharacterController
    {
       if (characterControl == null) //only get component if it is not already set
        {

             characterControl = animator.GetComponentInParent<Character>();

             //characterController looks at animator
            
        }
        return characterControl; //method will return the component of the parent which is called upon
    }

}
