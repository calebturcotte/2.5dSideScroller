using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWitch : BossHealthManager
{
    public float moveSpeed; //movement speed of the enemy
    public GameObject minion;
    public GameObject bullet;

    public float teleportTime; //how long it takes for the boss to teleport
    private Rigidbody enemyRB;
    private Rigidbody playerRB;
    private state currentState;
    private int teleportcount = 0; // counts how many teleports happened before spawning an enemy

    private float bulletangle = 0; //used to space out spawned bullets

    private float timeCounter = 0;

    //declare states for the different boss phases
    private enum state
    {
        PHASE1,
        PHASE2
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = state.PHASE1;
        //get child in position 0, the body of the object
        enemyRB = this.transform.GetChild(0).GetComponent<Rigidbody>(); //get the component of this enemy's rigidbody. No need to set in the inspector
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>(); //gets player's rigidbody. No need to set in the inspector

        // Starting in 2 seconds.
        // a projectile will be launched every 1 second
        InvokeRepeating("LaunchProjectile", 2.0f, 1f);
    }

    // Update is called once per frame
    public override void Update()
    {
        
        Vector3 playerDirection = playerRB.transform.position - enemyRB.transform.position;
        timeCounter += Time.deltaTime;


        switch (currentState)
        {
            case state.PHASE1:
                float x = Mathf.Cos(timeCounter);
                float y = Mathf.Sin(timeCounter);
                transform.Translate(playerDirection * moveSpeed * Time.deltaTime);
                enemyRB.transform.Translate( new Vector3(x, y, 0) * Time.deltaTime);
                break;

            case state.PHASE2:
                //Teleport 3 times then spawn a basic enemy
                
                if(timeCounter >= teleportTime)
                {
                    teleportcount = (teleportcount + 1 )%3;
                    timeCounter = 0;
                    transform.position = playerDirection / 3 + transform.position;

                    if(teleportcount == 2)
                    {
                        Instantiate(minion, new Vector3(transform.position.x, transform.position.y+3, 0), transform.rotation); 
                    }
                }
                
                break;
        }
        
        //update state based on bosses health
        if (base.currentHealth <= base.characterMaxHealth/2 && currentState != state.PHASE2)
        {
            timeCounter = 0;
            currentState = state.PHASE2;
        }
    }

    //Launch some bullets
    private void LaunchProjectile()
    {
        for (int i = 0; i < (Random.Range(1,3)); i++)
        {
            Instantiate(bullet, new Vector3(Mathf.Cos(bulletangle)*2, Mathf.Sin(bulletangle)*2, 0) + transform.position, transform.rotation);
            bulletangle = (bulletangle + 120) % 360;
        }

    }

    //Override IsColliding since it isn't a simple check
    public override bool IsColliding()
    {
        return false;
    }

    //Override IsGrounded since it isn't a simple check
    public override bool IsGrounded()
    {
        return false;
    }
}
