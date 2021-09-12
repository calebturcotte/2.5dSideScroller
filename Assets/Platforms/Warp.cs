using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    /**
     * Saves the location of the other warp pad to our player
     */
    public GameObject destination;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<Player>().warpDestination = destination;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<Player>().warpDestination = null;
        }
    }
}
