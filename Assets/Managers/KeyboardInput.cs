using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : Singleton<KeyboardInput>
{
    public KeyCode jump, moveLeft, moveRight, grapple, shoot;
    

    void Update()
    {
        if(Input.GetKey(moveRight))
        {
            VirtualInputManager.Instance.moveRight = true;
        } else
        {
            VirtualInputManager.Instance.moveRight = false;
        }

        if (Input.GetKey(moveLeft))
        {
            VirtualInputManager.Instance.moveLeft = true;
        }
        else
        {
            VirtualInputManager.Instance.moveLeft = false;
        }

        if (Input.GetKey(shoot))
        {
            VirtualInputManager.Instance.shoot = true;
        }
        else
        {
            VirtualInputManager.Instance.shoot = false;
        }

        if (Input.GetKey(grapple))
        {
            VirtualInputManager.Instance.grapple = true;
        }
        else
        {
            VirtualInputManager.Instance.grapple = false;
        }

        if (Input.GetKeyDown(jump))
        {
            VirtualInputManager.Instance.jump = true;
        }
        else
        {
            VirtualInputManager.Instance.jump = false;
        }




    }
}
