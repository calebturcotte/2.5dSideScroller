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
    private Player enemyMove; //giving access to player
    private Rigidbody enemyrb;

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
        enemyMove = this.GetComponent<Player>();
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

        if (patrolling)
        {
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

            if(!Physics.Raycast(enemyrb.transform.position - new Vector3(0.5f, 0, 0), leftcheck * 3f, out hit, 2))
            {
                if (hit.collider == null) // change direction if no platform is found
                {
                    movestate = true;
                }

            }
            else if(!Physics.Raycast(enemyrb.transform.position + new Vector3(0.5f, 0, 0), rightcheck * 3f, out hit, 2))
            {
                if (hit.collider == null) // change direction if no platform is found
                {
                    movestate = false;
                }
            }
            else if (Physics.Raycast(enemyrb.transform.position, Vector3.left * 3f, out hit, walldistance))
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    Physics.Raycast(enemyrb.transform.position, new Vector3(-1, 1, 0) * 3f, out hit, walldistance);
                    if (hit.collider == null)
                    {
                        enemyMove.jump = true;
                    }
                    else
                    {
                        movestate = true;
                    }

                }
                else if (!hit.collider.CompareTag("Player"))
                {
                    movestate = true;
                }
            }
            else if (Physics.Raycast(enemyrb.transform.position, Vector3.right * 3f, out hit, walldistance))
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    Physics.Raycast(enemyrb.transform.position, new Vector3(1, 1, 0) * 3f, out hit, walldistance);
                    if (hit.collider == null)
                    {
                        enemyMove.jump = true;
                    }
                    else
                    {
                        movestate = false;
                    }
                }
                else if (!hit.collider.CompareTag("Player"))
                {
                    movestate = false;
                }
            }
            else if (Physics.Raycast(enemyrb.transform.position, Vector3.left * 3f, out hit, spotdistance, ~IgnoredEnemy))
            {

                if (hit.collider.CompareTag("Player"))
                {
                    patrolling = false;
                }
            }
            else if (Physics.Raycast(enemyrb.transform.position, Vector3.right * 3f, out hit, spotdistance, ~IgnoredEnemy))
            {

                if (hit.collider.CompareTag("Player"))
                {
                    patrolling = false;
                }
            }
            else if (Physics.Raycast(enemyrb.transform.position, new Vector3(-1,1,0) * 3f, out hit, spotdistance, ~IgnoredEnemy))
            {

                if (hit.collider.CompareTag("Player"))
                {
                    patrolling = false;
                }
            }
            else if (Physics.Raycast(enemyrb.transform.position, new Vector3(1,1,0) * 3f, out hit, spotdistance, ~IgnoredEnemy))
            {

                if (hit.collider.CompareTag("Player"))
                {
                    patrolling = false;
                }
            }
/*            Debug.DrawRay(enemyrb.transform.position - new Vector3(0.5f,0,0), leftcheck, Color.red, 0.5f);
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
}
