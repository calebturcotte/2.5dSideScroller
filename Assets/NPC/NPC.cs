using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    /**
     * NPC script responsible for handling NPC's vision/reactions
     */
    public string dialogue;
    private TextMesh myText;
    private bool started;

    private void Start()
    {
        myText = transform.parent.GetComponentInChildren<TextMesh>();
        started = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if(!started)
            {
                EventTrigger(other.gameObject);
            }
            
        }
    }

    // Trigger event caused by the Player entering npc's field of view
    public virtual void EventTrigger(GameObject player)
    {
        StartCoroutine(addText(dialogue));
    }

    // Adds text to TextMesh by character over time to have a more fluid effect
    public IEnumerator addText(string textAdded)
    {
        if(textAdded.Length <= 0)
        {
            yield return null;
        }
        started = true;

        string currentText = "";

        for (int i = 0; i < textAdded.Length; i++)
        {
            currentText += textAdded[i];

            yield return new WaitForSeconds(0.05f);

            myText.text = currentText;
        }

        //Have text dissapear after 4 seconds

        yield return new WaitForSeconds(4f);
        myText.text = "";
        started = false;
    }
}
