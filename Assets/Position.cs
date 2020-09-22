using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    Vector3 mousePos;
    public Camera cam;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        mousePos =Input.mousePosition; // position of our mouse on the screen, where bottom left is 0,0
        
        //Debug.Log();

    }

    void FixedUpdate()
    {
        Vector3 lookDir = mousePos - Camera.main.WorldToScreenPoint(player.transform.position);
        Vector3 new_position = lookDir.normalized * 1f;
        transform.position = new_position + player.transform.position;
        
        transform.eulerAngles = new Vector3(0,0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f);


    }
}
