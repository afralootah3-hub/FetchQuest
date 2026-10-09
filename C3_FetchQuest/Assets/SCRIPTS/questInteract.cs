using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class questInteract : MonoBehaviour
{

    /* TO DO:
     * set the storyData phase for my YarnSpinner script to interpret
     * Program trigger functionality                                                DONE
     * read input from my player to initialize the interaction 
     * 
    */

    public TextMeshProUGUI interactText;
    public bool canInteract;
    public updateDialogPhase changePhase;
    

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // do something
            setText("Press E");
            canInteract = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // do something
            setText("");
            canInteract = false; 
        }
    }

    public void setText(string text)
    {
        // set the text of a tmpUGUI to the text arguement of this method 
        interactText.text = text;
    }

    public void Update()
    {
        if (canInteract)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("Interacting with " + this.name);

                changePhase.Invoke("quest_completed");
                setText("");
                canInteract = false;
            }
        }
    }


}

[System.Serializable]
public class updateDialogPhase : UnityEvent<string>
{

}