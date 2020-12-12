using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Entered : MonoBehaviour
{
    /**
     * Our check for if the player has entered a room, if room hasn't been cleared yet then entering the room
     * will cause the camera to be set to a fixed position while door close and boss fight starts
     */

    public Transform camTarget;
    public Camera ourcamera;
    public bool alreadyentered;

    public List<GameObject> Doors;

    public GameObject enemyBoss;

    private void Start()
    {
        alreadyentered = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (alreadyentered == true)
        {
            return;
        }


        if (other.tag.Equals("Player"))
        {
            alreadyentered = true;
            //ourcamera.GetComponent<CameraFollow>().followplayer = false;
            ourcamera.GetComponent<CameraFollow>().roomTarget = camTarget;
            ourcamera.GetComponent<CameraFollow>().Transition(false);

            //turn on collider and mesh renderer for each door object we specify in the room
            foreach(GameObject Door in Doors)
            {
                Door.GetComponent<BoxCollider>().enabled = true;
                Door.GetComponent<MeshRenderer>().enabled = true;
            }

            GameObject enemy = Instantiate(enemyBoss,new Vector3(camTarget.position.x,camTarget.position.y,0), camTarget.rotation); //firePoint.position, firePoint.rotation);
            //Should make a new script for enemy movement that is different than the one used for players
            enemy.GetComponent<Character>().cam = ourcamera;

            enemy.GetComponent<BossHealthManager>().masterRoom = gameObject;

        }
    }

    // Called when a boss is destroyed
    public void onBossDefeated()
    {
        ourcamera.GetComponent<CameraFollow>().Transition(true);

        //turn on collider and mesh renderer for each door object we specify in the room
        foreach (GameObject Door in Doors)
        {
            Door.GetComponent<BoxCollider>().enabled = false;
            Door.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
