using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default", menuName = "Inventory/Items/Default")]
public class ItemDefault : ItemObject
{
    public void Awake()
    {
        type = ItemType.Default;
    }


}
