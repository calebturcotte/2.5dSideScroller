using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Items/Food")]
public class ItemFood : ItemObject
{
    public int healValue;
    public void Awake()
    {
        type = ItemType.Food;
        healValue = 0;

    }


}

