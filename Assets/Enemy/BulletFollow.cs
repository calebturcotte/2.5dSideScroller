using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollow : MonoBehaviour
{
    public float moveSpeed;
    public int damage;
    private Rigidbody playerRB;
    private float m_Lifespan = 3f; //bullet lifespan, in seconds


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();

        //remove our bullet after lifespan time
        Destroy(gameObject, m_Lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = playerRB.transform.position - transform.position;
        transform.Translate(playerDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character health = other.GetComponent<Character>();
            health.DamageTaken(damage);
        }
        Destroy(gameObject);
    }
}
