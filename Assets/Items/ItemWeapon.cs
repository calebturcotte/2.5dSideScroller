using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Items/Weapon")]
public class ItemWeapon : ItemObject
{
    public int str;
    public int def;
    public void Awake()
    {
        type = ItemType.Food;
        str = 0;
        def = 0;
    }

}
