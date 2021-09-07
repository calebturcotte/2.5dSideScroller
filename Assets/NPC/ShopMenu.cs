using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopMenu : MonoBehaviour
{
    public InventoryObject playerInventory;
    public Player player;
    public GameObject shopMenuUI;
    public Text text;

    // buy an item from shopkeep [Currently function inside Player is used instead]
    public void buyItem(ItemObject item)
    {
        if (item.price <= player.currency)
        {
            playerInventory.AddItem(item, 1);
            Debug.Log("Item added");
            text.text = "Thanks for your purchase!";
        }
        else
        {
            // Not enough funds
            Debug.Log("Not enough funds: " + player.currency);
            text.text = "Not enough funds!";
        }
        UnityEditor.SceneView.RepaintAll();

    }

    public void exitMenu()
    {
        shopMenuUI.SetActive(false); //deactivate the pause menu
        Player.isShopping = false;
        Player.GamePaused = false; //set gamePaused to FALSE (may be needed to pause non-time related elements)
        Time.timeScale = 1f; //set timescale to 1; normal speed
    }



}
