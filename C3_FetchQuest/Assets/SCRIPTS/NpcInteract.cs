using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;
//
public class NpcInteract : MonoBehaviour
{
    // TO DO: 
    /* Get storyData to 'set' the phase of our YarnSpinner Scripts      DONE
     * set storyData from YarnSpinner Scripts                           DONE
     * 
     */

    public TextMeshProUGUI promptText;
    public string interactText;
    public bool canInteract;
    public DialogueRunner npcDialogue;
    public NpcCamRig npcCamRig;
    public npcData storyData; 

    public void Start()
    {
        //setting this variable looks a bit 'static' for our system design tastes
        //BUT we can practice a bit of deliberate design because we know we're
        //creating a systematized NPC rig & Interaction for multiple instances

        npcCamRig = transform.GetComponentInChildren<NpcCamRig>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //prompt interaction pops up 
            setText(interactText);
            canInteract = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //reset everything
            setText("");
            canInteract = false;
        }
    }

    //using an internal setter class
    public void setText(string txt)
    {
        promptText.text = txt;
    }


    // Update is called once per frame
    void Update()
    {

        if (canInteract)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                npcDialogue = FindFirstObjectByType<DialogueRunner>();


                npcDialogue.StartDialogue(storyData.startingNode); //passing in the node (Case sensitive)
                npcCamRig.movePlayer();
                Debug.Log("START NPC DIALOGUE");
                setText("");
                //load in dialogue system
                
                
                
                canInteract = false;
                
            }
        }

    }

    /// <summary>
    /// 'sets' the $dialogPhase variable in YarnSpinner script
    /// </summary>

    //used in 'loadPhase' node to route the YarnSpinner script its correct story path
    [YarnCommand("setPhase")]
    public void setStoryPhase()
    {
        InMemoryVariableStorage vStore = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
        vStore.SetValue("$dialogPhase", storyData.currentPhase.ToString());
    }

    /// <summary>
    /// 'gets' a string from YarnSpinner and 'converts' it to a dialogPhase enum
    /// </summary>
    /// <param name="phase"></param>

    //converts to the phase we're on in our npcData scritpableobj
    //also contains a nice error reporting in case a string is passed that does not
    //correspond to any dialogphase
    [YarnCommand("getPhase")]
    public void getStoryPhase(string phase)
    {
        PlayerUI.instance.pUiUpdate(phase); //send current phase to pui
        Debug.Log(phase);
        if(phase == "comeback")
        {
            storyData.currentPhase = npcData.dialogPhase.comeback;
        }
        else if(phase == "quest_started")
        {
            storyData.currentPhase = npcData.dialogPhase.quest_started;
            
        }
        else if(phase == "quest_completed")
        {
            storyData.currentPhase = npcData.dialogPhase.quest_completed;
        }
        else
        {
            Debug.LogError("The string you passed in the Yarn Script does not match any current dialogPhase in the npcData ScriptableObject");
        }


    }

    [YarnCommand("GetItem")]
    public void getItemNeeded(string itemName)
    {
        //gets the item you need to complete the quest, playerInventory now sends the message that quest is complete

    }

    [YarnCommand("SetItem")]
    public void setItemNeeded(string itemNeededName)
    {
        //sets the item you need to complete the quest
    }


    public void OnApplicationQuit()
    {
        storyData.dataReset();
    }


}
