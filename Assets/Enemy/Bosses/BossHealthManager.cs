using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : Character
{
    /**
     * Subclass of EnemyHealth Manager that calls any events if enemy is destroyed
     */

    public GameObject masterRoom;

    //Override for 
    public override void DamageTaken(int damage)
    {
        base.DamageTaken(damage);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Room_Entered temproom = masterRoom.GetComponent<Room_Entered>();
            temproom.onBossDefeated();
        }
    }


}
