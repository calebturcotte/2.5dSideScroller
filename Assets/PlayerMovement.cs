using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /**
     * Our Handler for player movement
     */
    public float movespeed;
    public int health;
    public enum TransitionParameter
    {
        walk,
        shoot,
        jump,
        jumpTransition,
        jumpLanding,
        grounded,
        grappling,
    }
    public bool moveRight;
    public bool moveLeft;
    public bool grapple;
    public bool grappling;
    public bool shoot;
    public bool jump;
    public bool grounded;
    


    public Animator animator; //var represents the animator object and controls it
   
    private Rigidbody rb;
    public Camera cam;

    public Vector3 aimingposition;


    public Vector3 movement;
    public Vector3 mousePos;




    void Update()
    {
       

        //currently gives position of mouse on camera
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Input.mousePosition; //gives position where camera bottom left corner is 0,0
        mousePos.z = 1;

        aimingposition = (mousePos - Camera.main.WorldToScreenPoint(transform.position)).normalized * 1f;
        aimingposition.z = 0;

    }

    public Rigidbody BiggRigid
    {
        get
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            return rb;
        }
    }

    public GameObject ColliderEdgePrefab;
    public List<GameObject> BottomSpheres;
    private void Awake()
    {
        BoxCollider box = GetComponent<BoxCollider>(); //get component of the box collider

        float bottom = box.bounds.center.y - box.bounds.extents.y; //defintes the length from center to bottom of box
        float top = box.bounds.center.y + box.bounds.extents.y;//defintes the length from center to top of box
        float front = box.bounds.center.x + box.bounds.extents.x;//defintes the length from center to front (x) of box
        float back = box.bounds.center.x - box.bounds.extents.x;//defintes the length from center to back (x) of box

        GameObject bottomFront = CreateEdgeSphere(new Vector3(front, bottom, 0f));
        GameObject bottomBack = CreateEdgeSphere(new Vector3(back, bottom, 0f));

        bottomFront.transform.parent = this.transform;
        bottomBack.transform.parent = this.transform;

        BottomSpheres.Add(bottomFront);
        BottomSpheres.Add(bottomBack);
    }

    public GameObject CreateEdgeSphere(Vector3 pos)
    {
        GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);
        return obj;
    }

}
