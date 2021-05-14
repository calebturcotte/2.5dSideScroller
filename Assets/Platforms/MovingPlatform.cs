using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    /**
     * Script for moving a platform between points a to b
     * Can be improved to have a List<> of vectors then it will slowly move to each point in that list
     */
    private Vector3 PointA;
    public Transform PointB;
    private bool towardsA;
    public int moveSpeed; //the speed we move the platform
    private Vector3 aimingposition;
    void Start()
    {
        towardsA = true;
        PointA = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (towardsA)
        {
            aimingposition = (PointA - PointB.position).normalized;
            transform.Translate(aimingposition * moveSpeed * Time.deltaTime); //translation
            //since the positions won't become exactly equal we use a vector distance to find when they're close enough to move the other direction
            if (Vector3.Distance(transform.position, PointA) < 0.1)
            {
                towardsA = false;
            }
        }
        else
        {
            aimingposition = (PointB.position - PointA).normalized;
            transform.Translate(aimingposition * moveSpeed * Time.deltaTime); //translation
            if (Vector3.Distance(transform.position, PointB.position) < 0.1)
            {
                towardsA = true;
            }

        }
        
    }
}
