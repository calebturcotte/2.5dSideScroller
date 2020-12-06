using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ManualInput : MonoBehaviour
{
    private Player playerMove; //giving access to Player
  
    private void Awake()
    {
        playerMove = this.GetComponent<Player>();
        //test = this.GetComponent<itemHandler>();
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
