using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBehaviour : MonoBehaviour
{

    public int spotdistance;
    public int walldistance;
    public int shootdistance;
    public int gonedistance;
    public GameObject bulletPrefab;
    public LayerMask IgnoredEnemy;

    private float bulletForce = 1f;

    private GameObject player;
    private Character enemyMove; //giving access to character
    private Rigidbody enemyrb;
    public Rigidbody playerRB;

    private bool patrolling;

    RaycastHit hit;

    Vector3 leftcheck;

    Vector3 rightcheck;

    private bool movestate;

    private float waittime;
    private float timewaited;

    private float bullettime;
    private float bulletcount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyMove = this.GetComponent<Character>();
        enemyrb = this.GetComponent<Rigidbody>();
        leftcheck = new Vector3(-1, -1, 0);
        rightcheck = new Vector3(1, -1, 0);
        patrolling = true;
        movestate = true;
        waittime = 1f;
        timewaited = 0;

        bullettime = 1f;
        bulletcount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        enemyMove.jump = false;
        if (patrolling)
        {
            //Enemy moves in left or right directions
            if (movestate)
            {
                enemyMove.moveRight = true;
                enemyMove.moveLeft = false;
            }
            else
            {
                enemyMove.moveRight = false;
                enemyMove.moveLeft = true;
            }

            checkFront();

            /*            Debug.DrawRay(enemyrb.transform.position - new Vector3(0.5f, 0, 0), leftcheck, Color.red, 0.5f);
                        Debug.DrawRay(enemyrb.transform.position + new Vector3(0.5f, 0, 0), rightcheck, Color.red, 0.5f);*/
        }
        else
        {
/*            Debug.DrawRay(enemyrb.transform.position, Vector3.left * 3f, Color.red, 0.5f);*/
            Vector3 distance = enemyrb.transform.position - player.GetComponent<Rigidbody>().transform.position;
            if (Vector2.Distance(enemyrb.transform.position, player.GetComponent<Rigidbody>().transform.position) < gonedistance)
            {
                timewaited = 0;
                //Debug.Log(distance);
                if (distance.x > shootdistance)
                {

                    enemyMove.moveRight = false;
                    enemyMove.moveLeft = true;
                }
                else if (distance.x < -shootdistance)
                {
                    enemyMove.moveRight = true;
                    enemyMove.moveLeft = false;
                }
                else
                {
                    enemyMove.moveRight = false;
                    enemyMove.moveLeft = false;
                    if (bulletcount > bullettime)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, enemyrb.transform.position - distance.normalized * 1, enemyrb.transform.rotation); //firePoint.position, firePoint.rotation);
                        Rigidbody rb = bullet.GetComponent<Rigidbody>();
                        rb.AddForce(-distance * bulletForce, ForceMode.Impulse);
                        bulletcount = 0;
                    }
                    bulletcount += Time.deltaTime;
                }

            }
            else
            {
                timewaited += Time.deltaTime;
                enemyMove.moveRight = false;
                enemyMove.moveLeft = false;
                if(timewaited > waittime)
                {
                    patrolling = true;
                }
            }
        }
    }

    /**
     * Check areas in front of the Enemy and update State Accordingly
     */
    void checkFront()
    {
        Vector3 facing;
        Vector3 groundCheck;
        Vector3 platformCheck;

        //set our variables based on direction we face
        if (movestate)
        {
            facing = Vector3.right;
            groundCheck = new Vector3(1, 1, 0);
            platformCheck = rightcheck;
        }
        else
        {
            facing = Vector3.left;
            groundCheck = new Vector3(-1, 1, 0);
            platformCheck = leftcheck;
        }

        Debug.DrawRay(enemyrb.transform.position, facing * 2f, Color.red);
        Debug.DrawRay(enemyrb.transform.position, groundCheck * 3f, Color.red);

        Vector3 playerLocation = playerRB.transform.position - enemyrb.transform.position; 

        if (Physics.Raycast(enemyrb.transform.position, facing * 2f, out hit, walldistance))
        {
            if (hit.collider.CompareTag("Platform"))
            {
                if(Physics.Raycast(enemyrb.transform.position, groundCheck * 3f, out hit, walldistance))
                {
                    movestate = !movestate;
                }
                else
                {
                    //Debug.Log(enemyMove);
                    enemyMove.jump = true;
                }
            }
            else if (!hit.collider.CompareTag("Player"))
            {
                movestate = !movestate;
            }
        }
        else if (!Physics.Raycast(enemyrb.transform.position, platformCheck * 3f, out hit, 2))
        {
            if (hit.collider == null) // change direction if no platform is found at ground level
            {
                movestate = !movestate;
            }

        }
        else if (Physics.Raycast(enemyrb.transform.position, playerLocation, out hit, spotdistance, ~IgnoredEnemy))
        {
            if (hit.collider.CompareTag("Player"))
            {
                patrolling = false;
            }
        }
    }
}
