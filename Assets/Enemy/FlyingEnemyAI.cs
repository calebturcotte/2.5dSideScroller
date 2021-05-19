using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingEnemyAI : Character
{
    /*variables describing the enemy's state; mostly for ai*/
    private bool patrolling; //important for controlling movement
    private bool airborn; //this state is not used for now
    private float moveDist; //current distance moved along preset "track"

    /*components for calculations*/
    private Rigidbody enemyRB; //THIS flying enemy's rigidbody component
    private Rigidbody playerRB; //the PLAYER's rigidbody component


    /*enemy parameters that can be tweaked*/
    public float maxMoveDist; //maximum allocated movement distance for enemy
    public float moveSpeed; //movement speed of the enemy
    public float shootThreshold; //detection range of enemy + shooting range

    public GameObject bulletPrefab; //prefab for the bullet that will be shot by enemy
    public float bulletTimer; //timer that starts when a bullet is fired
    public float bulletCooldown; //cooldown time before a bullet can be fired again
    public float bulletForce; //force at which the bullet will be shot



    
    void Start()
    {
        patrolling = true; //at the start, patrol
        airborn = true; //start airborn. Doesn't mean much for now
        enemyRB = this.GetComponent<Rigidbody>(); //get the component of this enemy's rigidbody. No need to set in the inspector
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>(); //gets player's rigidbody. No need to set in the inspector
        direction = 1; //standard initial direction is RIGHT
    }

    // Update is called once per frame
    public override void Update()
    {
        
        base.Update(); //call all update functions from the parent class. Nothing for now

        /* enemy ai starts here */
        if(patrolling) //if patrolling state is true (always at the start)
        {
            if (PlayerScanner()) //check for the player before anything else
            { //if you find a player, stop patrolling and exit this loop
                FaceThePlayer(); // face towards the player based on position
                patrolling = false; //stop patrolling --> will force loop to engage the player
                return; // leave the loop and on the next frame, engage player
            } 
            //otherwise if patrolling, do collision check which includes movement
            CollisionCheck();

            moveDist += Time.deltaTime; //increase moveDist with time
            if (moveDist >= maxMoveDist) //once moveDist = maxMoveDist, change directions
            {
                ChangeDirection(); //change enemy's direction
                moveDist = 0f; //reset moveDist
            }

        } else //if patrolling is false, engage the player
        {
            if(playerRB != null) //if player has not been drestroyed
            {
            Engage();
            }
        }

    }

    void Engage() //fights with player; should be calling on the shoot script
    {    
        Vector3 shootDirection = enemyRB.transform.position - playerRB.transform.position;
        //get vector that points towards player
        if ((shootDirection.magnitude > shootThreshold))   
        { //will almost never be called; checks if player is too far away
            patrolling = true; //briefly turn on patrolling so enemy will edge closer to the player
            /*note: patrolling will likely turn off again within a few frames
            May cause stuttering movement since patrolling is constantly turned on and off here*/
            EnemyMove();
        }
        else //otherwise player is in shooting range
            {
            if (bulletTimer >= bulletCooldown) //once timer hits cooldown threshold, fire
            {
                Fire(shootDirection); //shoot bullet
                bulletTimer = 0f; //reset timer
            }
            moveDist = 0f; //reset moveDist
            /*Since enemy will be facing player, it will continue chasing forever this way
            Enemy will NOT reset to default position with this implementation.
            Could add an empty to prefab such that the enemy returns to starting position*/
            bulletTimer += Time.deltaTime;
        }
        
    }

    public override void OnCollisionEnter(Collision collision) //upon a collision
    {
        if(collision.collider.CompareTag("PlayerBullet")) //if hit by a player bullet
        {
            FaceThePlayer();
        }

        if(collision.collider.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider); //ignore collision with fellow enemies
        }
        //rigidbodies are in effect here so enemy will be pushed back and might fight the physics
        //could add grounded conditions here
    }


    void FaceThePlayer()
    {
        if ((enemyRB.transform.position - playerRB.transform.position).x < 0)
        { //if the enemy is to the left of the player
            direction = 1; //face right; face the player
        }
        else //otherwise enemy is to the right of the player
        {
            direction = -1; //otherwise face left, face the player
        }
    }
    void Fire(Vector3 shootDirection) //fire bullet
    {
        GameObject bullet = Instantiate(bulletPrefab, enemyRB.transform.position - shootDirection.normalized * 1, enemyRB.transform.rotation); //firePoint.position, firePoint.rotation);   
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(-shootDirection * bulletForce, ForceMode.Impulse); //change this to the shoot script
    }

    void EnemyMove()
    {
        if (direction == 1)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        } else if (direction == -1)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    } //handles enemy movement. Can switch to animator's movement

    void CollisionCheck()
    {
        if (IsColliding()) //basically if near a wall
        {
            ChangeDirection(); //face the other way
        }
        EnemyMove(); //start moving based on the new direction
    } //if the enemy is colliding, change direction

    void ChangeDirection() //changes direction of enemy if needed
    {
        if (direction == -1) //if facing LEFT
        {
            direction = 1; //change direction to RIGHT
        }
        else if (direction == 1) //otherwise if facing RIGHT
        {
            direction = -1; //change direction to LEFT
        }
    }

    public override bool IsColliding()  //collision detection method 
    {
        BoxCollider box = GetComponent<BoxCollider>(); //get component of the box collider
        LayerMask myMask = (8 & 11);// transparent layer we want to avoid stopping for, add other Strings inside brackets for other layers we may need to ignore
        Vector3 correction = new Vector3(0, 0.1f, 0);
        bool boxCastHit = Physics.BoxCast(box.bounds.center, box.bounds.extents - correction, Vector3.right * direction, Quaternion.identity, 0.1f, ~myMask); //seems to be the most precise way to do this so far
        //boxcase should be almost as big as the box -0.1f from the top and bottom to avoid ground collision errors
        return boxCastHit; //return the value of boxCastHit
    }

    public bool PlayerScanner()
    {
        Vector3 shootDirection = playerRB.transform.position - enemyRB.transform.position;
        BoxCollider box = GetComponent<BoxCollider>(); //get component of the box collider
        LayerMask myMask = LayerMask.GetMask("Player"); //get a layer mask set to the "player"
        bool playerScanner = Physics.Raycast(box.bounds.center, shootDirection, shootThreshold, myMask);
        //bool playerScanner = Physics.BoxCast(box.bounds.center + box.bounds.extents * 1.5f * direction, transform.localScale, shootDirection, Quaternion.identity, shootThreshold, myMask); //seems to be the most precise way to do this so far
        //scans as far as the enemy can shoot + box is 1.5x size of the enemy
        return playerScanner;//return the value of playerScanner
    }
}
