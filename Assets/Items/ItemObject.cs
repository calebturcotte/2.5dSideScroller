using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
   Default, 
    Food,
    Comsumable,
    Weapon,
    Key
}


public abstract class ItemObject : ScriptableObject
{
    public ItemType type;
    public string itemName = "New Item";
    [TextArea(15, 20)]
    public string itemDescription = "New Description";
    public int price = 0;

}
