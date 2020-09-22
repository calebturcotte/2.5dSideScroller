using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    /**
     * Our Handler for projectiles fired such as grappling hooks or bullets
     */
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject grapplePrefab;

    private Rigidbody grapplerb;
    private LineRenderer rope;
    private GameObject grapple;

    public float bulletForce = 20f;
    public float grappleForce = 10f;

    bool grappling;
    float grapplingtime = 0.5f;
    float grapplingtimespent;
    float mingrapplingtime = 0.05f;
    bool collided;

    private void Start()
    {
        collided = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        else if (Input.GetButtonDown("Fire2") && !grappling)
        {
            grappling = true;
            Grapple();
        }

        if (grappling)
        {
            rope = GetComponent<LineRenderer>();
            rope.positionCount = 2;
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, grapplerb.transform.position);
            grapplingtimespent += Time.deltaTime;
            //after enough time passes delete/return our grappling hook
            if(grapplingtimespent > grapplingtime)
            {
                GrappleEnd();
            }
            else if (grapplingtimespent < mingrapplingtime && collided)
            {
                GrappleEnd();
            }
        }
    }

    //Our function that fires a bullet from our main character
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode.Impulse);
    }

    void Grapple()
    {
        grapple = Instantiate(grapplePrefab, firePoint.position, firePoint.rotation);
        grapplerb = grapple.GetComponent<Rigidbody>();
        grapplerb.AddForce(firePoint.up * grappleForce, ForceMode.Impulse);
    }

    public void GrappleEnd()
    {
        rope.positionCount = 0;
        grappling = false;
        grapplingtimespent = 0;
        collided = false;
        Destroy(grapple);
        
    }

    public void GrappleHit()
    {
        collided = true;
    }
}
