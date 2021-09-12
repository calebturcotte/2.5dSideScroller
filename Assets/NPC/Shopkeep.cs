using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeep : NPC
{
    /**
     * Script lets players know if they are standing in front of
     * a shopkeep
     */
    //store items they have in their inventory
    public InventoryObject inventory;

    public override void EventTrigger(GameObject player)
    {
        base.EventTrigger(player);

        player.GetComponent <Player>().shopkeep = inventory;

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<Player>().shopkeep = null;
        }
    }

    
}
