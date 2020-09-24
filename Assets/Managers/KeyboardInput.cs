using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            VirtualInputManager.Instance.moveRight = true;
        } else
        {
            VirtualInputManager.Instance.moveRight = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            VirtualInputManager.Instance.moveLeft = true;
        }
        else
        {
            VirtualInputManager.Instance.moveLeft = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            VirtualInputManager.Instance.shoot = true;
        }
        else
        {
            VirtualInputManager.Instance.shoot = false;
        }

        if (Input.GetMouseButton(1))
        {
            VirtualInputManager.Instance.grapple = true;
        }
        else
        {
            
            VirtualInputManager.Instance.grapple = false;
        }




    }
}
