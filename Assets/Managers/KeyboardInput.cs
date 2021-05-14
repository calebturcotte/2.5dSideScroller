using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : Singleton<KeyboardInput>
{       //KeyCode jump, moveLeft, moveRight, grapple, shoot;
    
    void Update()
    {
        
        VirtualInputManager.Instance.moveRight = Input.GetButton("moveRight");
        VirtualInputManager.Instance.moveLeft = Input.GetButton("moveLeft");
        VirtualInputManager.Instance.shoot = Input.GetButton("shoot");
        VirtualInputManager.Instance.grapple = Input.GetButton("grapple");
        VirtualInputManager.Instance.jump = Input.GetButtonDown("jump");
        VirtualInputManager.Instance.dash = Input.GetButtonDown("dash");
        VirtualInputManager.Instance.pause = Input.GetButtonDown("pause");

    }
}
