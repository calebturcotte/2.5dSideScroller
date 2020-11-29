using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ManualInput : MonoBehaviour
{
    private PlayerMovement playerMove; //giving access to playerMovement

    private void Awake()
    {
        playerMove = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove.moveRight = VirtualInputManager.Instance.moveRight;
        playerMove.moveLeft = VirtualInputManager.Instance.moveLeft;
        playerMove.shoot = VirtualInputManager.Instance.shoot;
        if (VirtualInputManager.Instance.grapple)
        {
            playerMove.grapple = VirtualInputManager.Instance.grapple;
        }
        
        playerMove.jump = VirtualInputManager.Instance.jump;
        //a tracker for our right click button, when release we will let go of the grapple if needed
        playerMove.grappling = VirtualInputManager.Instance.grapple;
        playerMove.dash = VirtualInputManager.Instance.dash;

    }
}
