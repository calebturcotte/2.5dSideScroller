using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public Camera cam;
    public HealthBar healthBar;
    /**
     * Our handler for player-specific properties
     */
    public InventoryObject inventory;
    public Vector3 mousePos;

    private GameObject lastPlatform;
    public GameObject grappleObject;

    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOver;
    public GameObject shopMenuUI;
    public GameObject shopMenuLayout;
    public GameObject buttonPrefab;

    public InventoryObject shopkeep; // our shopkeep inventory
    public static bool isShopping;

    public override void Awake()
    {
        currentHealth = characterMaxHealth;
        healthBar.SetMaxHealth(characterMaxHealth);
        isShopping = false;
               
    }

    public override void Update()
    {

        //currently gives position of mouse on camera
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = Input.mousePosition; //gives position where camera bottom left corner is 0,0
        mousePos.z = 1;

        aimingposition = (mousePos - Camera.main.WorldToScreenPoint(transform.position)).normalized * 1f;
        aimingposition.z = 0;


      

        if (VirtualInputManager.Instance.pause
             && !isShopping)
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
        else if (shopkeep != null
                    && VirtualInputManager.Instance.jump)
        {
            // open up shop

            if (isShopping)
            {
                shopMenuLayout.SetActive(false);
                isShopping = false;
                GamePaused = false;
                Time.timeScale = 1;
            } else
            {
                shopMenuLayout.SetActive(true);
                Debug.Log("Pressed");
                GamePaused = true;
                Time.timeScale = 0;
                setUpShop();
            }
        }

        base.Update();
    }


    public override void DamageTaken(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            //Destroy(gameObject);
        }
    }

    public override void OnCollisionEnter(Collision collision) //for universal character collision interactions, see character script
    {

        bool grounded = IsGrounded();

        if (collision.gameObject.tag == "MovingPlatform" && grounded) // if the collision is with a platform and you're ON TOP
        {
            transform.SetParent(collision.gameObject.transform);
        }
        else
        {
            transform.SetParent(null);
        }


        //save the last platform we were on
        if((collision.gameObject.CompareTag("MovingPlatform") || collision.gameObject.CompareTag("Platform")) && grounded)
        {
            lastPlatform = collision.gameObject;
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


    //Returns the last platform the Player stood on
    public GameObject getLastPlatform()
    {
        return lastPlatform;
    }

    // Instantiate all shop buttons for the shop
    private void setUpShop()
    {
        if (isShopping)
        {
            return;
        }
        isShopping = true; //check that we only set up shop once

        foreach (Transform child in shopMenuUI.transform)
        {
            GameObject.Destroy(child.gameObject); // empty the menu before adding more
        }

        for (int i = 0; i < shopkeep.Container.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(shopMenuUI.transform);

            Text ButtonText = button.GetComponentInChildren<Text>();

            ButtonText.text = shopkeep.Container[i].item.itemName;

            // add onclick that checks item price and player inventory money



            Debug.Log(shopkeep.Container[i].item);
        }
    }


}

