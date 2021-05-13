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
    public Rigidbody bulletRB;

    void Start()
    {
        //remove our bullet after lifespan time
        Destroy(gameObject, m_Lifespan);
    }

    public void OnTriggerEnter(Collider collider)
    {
        //collision lets us access values of object that we hit
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); //can add an effect or new animation after hit
        //Destroy(effect, 5f);
        //Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider); //ignore the COLLISION physics from the bullet and its target
        Collider bulletCollider = bulletRB.GetComponent<Collider>();

        if (collider.CompareTag("Player") || collider.CompareTag("Enemy"))
        {
            Character health = collider.GetComponent<Character>();
            health.DamageTaken(damage);
            Physics.IgnoreCollision(bulletCollider, collider, true);
        }
        Destroy(gameObject); //destroy the bullet regardless of what it hits
    }

    //remove our object once it leaves the screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
