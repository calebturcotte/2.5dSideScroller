using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public KeyCode jump, moveLeft, moveRight,grapple;
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

       /* if (Input.GetKey(KeyCode.Space))
        {
            VirtualInputManager.Instance.shoot = true;
        }
        else
        {
            VirtualInputManager.Instance.shoot = false;
        }*/

        if (Input.GetKeyDown(grapple))
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
