using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    /**
     * manages the enemy health
     */

    public int currenthealth;

    public virtual void DamageTaken(int damage)
    {
        currenthealth -= damage;
        if(currenthealth < 0)
        {
            Destroy(gameObject);
        }
    }
}
