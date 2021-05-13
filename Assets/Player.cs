using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    public Camera cam;
    public HealthBar healthBar;
    /**
     * Our handler for player-specific properties
     */
    public InventoryObject inventory;
    public Vector3 mousePos;


    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOver;

    public override void Awake()
    {
        currentHealth = characterMaxHealth;
        healthBar.SetMaxHealth(characterMaxHealth);
               
    }

    public override void Update()
    {

        //currently gives position of mouse on camera
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Input.mousePosition; //gives position where camera bottom left corner is 0,0
        mousePos.z = 1;

        aimingposition = (mousePos - Camera.main.WorldToScreenPoint(transform.position)).normalized * 1f;
        aimingposition.z = 0;


      

        if (VirtualInputManager.Instance.pause)
        {
            if (GamePaused == false)
            {

                pauseMenuUI.SetActive(true);
                GamePaused = true;
                Time.timeScale = 0;

            }
            else if (GamePaused == true)
            {

                pauseMenuUI.SetActive(false);
                GamePaused = false;
                Time.timeScale = 1;
            }
        }

        base.Update();
    }


    public override void DamageTaken(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            gameOver.SetActive(true);
        }
    }

    public override void OnCollisionEnter(Collision collision) //for universal character collision interactions, see character script
    {


        if (collision.gameObject.tag == "MovingPlatform" && IsGrounded()) // if the collision is with a platform and you're ON TOP
        {
            transform.SetParent(collision.gameObject.transform);
        }
        else
        {
            transform.SetParent(null);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        transform.SetParent(null); //unparent character when not colliding with anything
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();

        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }


    public override void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }


}

