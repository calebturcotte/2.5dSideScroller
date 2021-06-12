using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public InventoryObject playerInventory;
    public GameObject shopMenuUI;

    // buy an item from shopkeep
    public void buyItem(ItemObject item)
    {
        playerInventory.AddItem(item, 1);
    }

    public void exitMenu()
    {
        shopMenuUI.SetActive(false); //deactivate the pause menu
        Player.isShopping = false;
        Player.GamePaused = false; //set gamePaused to FALSE (may be needed to pause non-time related elements)
        Time.timeScale = 1f; //set timescale to 1; normal speed
    }



}
