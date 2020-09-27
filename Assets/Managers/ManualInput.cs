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
        if (VirtualInputManager.Instance.moveRight)
        {
            playerMove.moveRight = true;
        } else {
            playerMove.moveRight = false;
        }

        if (VirtualInputManager.Instance.moveLeft)
        {
            playerMove.moveLeft = true;
        } else {
            playerMove.moveLeft = false;
        }

        if (VirtualInputManager.Instance.shoot)
        {
            playerMove.shoot = true;
        } else {
            playerMove.shoot = false;
        }

        if (VirtualInputManager.Instance.grapple)
        {
            //will exit grapple state once the grapple has finished
            playerMove.grapple = true;
        } 

        //a tracker for our right click button, when release we will let go of the grapple if needed
        playerMove.grappling = VirtualInputManager.Instance.grapple;
    


    }
}
