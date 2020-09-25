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
        playerMove.grapple = VirtualInputManager.Instance.grapple;
        playerMove.jump = VirtualInputManager.Instance.jump;


    }
}
