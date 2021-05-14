using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : MonoBehaviour
{
    /**
     *  Responsible for handling when players or enemies enter the "Blastzone"
     */

    //the damage this blastzone does to a player
    public int damage;

    public ParticleSystem particles;

    //Check and compare objects that collide with the blastzone
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            //Instantiate a particle effect
            Instantiate(particles, other.transform.position, Quaternion.identity);

            //destroy any enemies that hit the blastzone
            Destroy(other.gameObject);


        }
        else if (other.CompareTag("Player"))
        {
            //Instantiate a particle effect
            Instantiate(particles, other.transform.position, Quaternion.identity);

            //damage player and move them back to last platform they were on
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            health.DamageTaken(damage);

            Player player = other.GetComponent<Player>();
            other.gameObject.transform.position = player.getLastPlatform().transform.position + new Vector3(0, player.getLastPlatform().transform.localScale.y + 0.5f, 0);

            //delete the grapple hook if it exists
            if(player.grappleObject != null)
            {
              player.grappleObject.GetComponent<Grapple>().EndHook();
            }

            //reset the velocity of the object
            player.BiggRigid.velocity = new Vector3(0, 0, 0);
        }
    }
}
