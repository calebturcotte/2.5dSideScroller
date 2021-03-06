﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventoryObject : ScriptableObject //Inventories will be scriptable objects. Usable for tutorials/swapping inventories
{
    public List<InventorySlot> Container = new List<InventorySlot>(); //our list will hold items defined by InventorySlot

    //InventorySlot creates scriptable objects which correspond to item lists. 
    public void AddItem(ItemObject _item, int _quantity) //add item to container
    {
        for (int i = 0; i < Container.Count; i++) //Container is a list of items
        {
            if (Container[i].item == _item) //scan the list of items. If we already have it, ____
            {
                Container[i].AddAmount(_quantity); //add to qty, pass in the quantity to be added when AddItem is invoked
                return;
                //exit the function
            }
        }
            Container.Add(new InventorySlot(_item, _quantity)); //add to our list
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int quantity;
    public InventorySlot(ItemObject _item, int _quantity)
    {
        item = _item;
        quantity = _quantity;
    }

    public void AddAmount(int value)
    {
        quantity += value;
    }
}
