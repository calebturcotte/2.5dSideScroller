using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    //animation/effect to add after a hit
    //public GameObject hitEffect;

    private float m_Lifespan = 1f; //bullet lifespan, in seconds
    public int damage;

    void Start()
    {
        //remove our bullet after lifespan time
        Destroy(gameObject, m_Lifespan);
    }

    void OnCollisionEnter(Collision collision)
    {
        //collision lets us access values of object that we hit
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); //can add an effect or new animation after hit
        //Destroy(effect, 5f);
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth health = collision.collider.GetComponent<PlayerHealth>();
            health.DamageTaken(damage);
        }
        Destroy(gameObject);


    }

    //remove our object once it leaves the screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
