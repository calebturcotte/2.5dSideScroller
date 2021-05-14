using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public bool followplayer;
    public Transform roomTarget;

    private bool transitioning;

    private void Start()
    {
        transitioning = false;
        followplayer = true;
    }

    private void LateUpdate()
    {
        if (followplayer)
        {
            if (transitioning)
            {
                //slowly move the camera towards player then fix to that point once close enough
                transform.position += (target.position + offset - transform.position) / 50;
                if (Vector3.Distance(transform.position, target.position + offset) < 0.1)
                {
                    transitioning = false;
                }

            }
            else
            {
                transform.position = target.position + offset;
            }

        }
        else
        {
            if (transitioning)
            {
                //slowly move the camera towards roomView then fix to that point once close enough
                transform.position += (roomTarget.position - transform.position) / 100;
                if (Vector3.Distance(transform.position, roomTarget.position) < 0.1)
                {
                    transitioning = false;
                }

            }
            else
            {
                transform.position = roomTarget.position;
            }



        }
    }


    //enable camera transition
    public void Transition(bool followplayer)
    {
        this.followplayer = followplayer;
        transitioning = true;
    }

}
