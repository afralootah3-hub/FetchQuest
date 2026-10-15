using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // a singleton that handles persistent ui updates across scenes
    // updates according to the story phase
    // updates according to what objects/items the player has (just the quest item and reward item)

    public static PlayerUI instance { get; set; }
    public TextMeshProUGUI uiText;
    private string _inventoryText;
    public List<string> _inventoryItems; //stores everything in inventory

    public void Awake()
    {
        if(instance !=null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void pUiUpdate(string currentPhase)
    {
        //uses npcdata to determine what quest, and what phase
        if (currentPhase == "quest_started")
        {
            Debug.Log("Updating UI...");
            uiText.text = "Current Quest:Pick some fruit! \r\nInventory: " + _inventoryText;
        }
        else if(currentPhase == "quest_complete")
        {
            Debug.Log("Updating UI...");
            uiText.text = "Pick some fruit complete! \r\nInventory: " + _inventoryText;
        }




    }


    public void addItem(string item)
    {
        _inventoryItems.Add(item); //add item to inventory list
    }
}
