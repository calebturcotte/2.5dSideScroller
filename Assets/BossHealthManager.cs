using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : EnemyHealthManager
{
    /**
     * Subclass of EnemyHealth Manager that calls any events if enemy is destroyed
     */

    public GameObject masterRoom;

    //Override for 
    override public void DamageTaken(int damage)
    {
        currenthealth -= damage;
        if (currenthealth < 0)
        {
            Destroy(gameObject);
            Room_Entered temproom = masterRoom.GetComponent<Room_Entered>();
            temproom.onBossDefeated();
        }
    }


}
