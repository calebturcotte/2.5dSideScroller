using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    /**
     *  Propels the player in the air upon contact
     */

    public float force = 15f;
    private void OnCollisionEnter(Collision user)
    {

        if (user.gameObject.name == "Player" || user.gameObject.name  == "Enemy")
        {
            // Debug.Log("hit board");
            user.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Impulse);
        }
    }

}
