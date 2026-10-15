using StarterAssets;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; set; }
    public addCoins coinUpdate;

    public addCrates crateUpdate;
    public questInteract interactUpdate;

    //referencing the coinUi CLASS and not obj 
    public CoinUI coinInventory;
    public CrateUI crateInventory;
    public PlayerUI playerInventory;
    public int totalCoins = 0;
    public int totalCrates = 0;
    public int totalItems = 0; //used for the apples

    public GameObject ThirdPersonRig;

    public Transform mainStartingTransform;

    public Vector3 loadPos;
    public Quaternion loadRot;

    public List<string> coinsCollected;
    public List<string> itemsCollected;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;

            // this is a built-in 'action' within the SceneManager that requires a listener method 
            // that takes two argueemnts: Scene & LoadScenemode
            // this 'action' is called whennever a new scene is loaded !
            SceneManager.sceneLoaded += OnSceneLoad;
            DontDestroyOnLoad(this);
        }
    }

    /// <summary>
    /// called by StartDialogue event in DialogRunner class
    /// </summary>
    /// <param name="b"></param>
    /// 

    //we will toggle true/false in the event system to call whetehr we want to move or not
    public void setPlayerMovement(bool b)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ThirdPersonController tpc = player.GetComponent<ThirdPersonController>();

        if (b)
        {
            tpc.enabled = false;
            tpc.MoveSpeed = 0;
            Debug.Log("DISABLED MOVEMENT");
        }
        else
        {
            tpc.enabled = true;
            tpc.MoveSpeed = 2;
            Debug.Log("ENABLED MOVEMENTS");
        }
        
    }


    #region loading data section

    //this is the receiever method of the sceneloaded action
    // it checks for what scene has been loaded using the scene's build index number (in built profiles)
    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //add an additional condition to see whether or not we want to load from main transform or another location
        PlayerUI.instance.DestoryAllCollected();

        if(scene.buildIndex == 0) //exterior scene 
        {

            //below logic checks to see if data has been loaded into loadPos
            //if loadpos is blank then the player gets spawned at mainStartingTransform
            //if loadpos has data then the player gets spawned at the loadpos/loadrot spot
            if(loadPos == Vector3.zero) //if no data
            {
                //start from main starting pos
                // calls the instantiation method using dynamic arguements for the position and rotation 
                SpawnThirdPersonPrefab(mainStartingTransform.position, mainStartingTransform.rotation);


            }
            else //if there is data in loadpos
            {
                SpawnThirdPersonPrefab(loadPos, loadRot);
            }
            

            //loadCoins();
            //This singleton method is called by each individual coin prefab
            // it adds the coinUI class method called 'coinUpdate' as a listener to the 
            // coinUpdate event! That event also passes the totalCoins int as an arguement for that 'coinUpdate' method
            //coinUpdate.AddListener(coinInventory.coinUpdate);
            //crateUpdate.AddListener(crateInventory.crateUpdate);

        }


        
        //crateUpdate.AddListener(crateInventory.crateUpdate);
        //Debug.Log("crate ui is " + crateInventory);

    }


    /// <summary>
    /// called by the trigger of loading zones when exiting the scene
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    public void LoadLocationData(Vector3 pos, Quaternion rot)
    {
        //when we reload the '3D_Demo' scene we use the loadPos and loadRot data 

        loadPos = pos;
        loadRot = rot; 
    }

    /// <summary>
    /// a 'setter' method for 'coinInventory' variable
    /// </summary>
    /// <param name="ui"></param>
    public void loadCoinUI(CoinUI ui)
    {
        coinInventory = ui;


    }

    public void loadPlayerUI(PlayerUI pUI)
    {

    }

    /// <summary>
    /// A public method to instantiate third person rig prefab
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// 
    public void SpawnThirdPersonPrefab(Vector3 pos, Quaternion rot)
    {
        //it takes two arguements, position and rotation of where we want to spawn our ThirdPersonRig prefab 
        
        Instantiate(ThirdPersonRig, pos, rot); 




    }

    #endregion 
    public void Start()
    {

       

    }


    /// <summary>
    /// Takes coinId of prefab collected, and adds to list
    /// </summary>
    /// <param name="coinID"></param>
    /// 

    // it then invokes the ui to add a coin to the existing count 
    public void coinCollect(string coinID)
    {
        //Debug.Log("You've collected a coin :D"); 
        totalCoins++;

        coinsCollected.Add(coinID);
        
        
        //invoking the unity event
        coinUpdate.Invoke(totalCoins);
    }

    public void itemCollect(string itemID)
    {
        totalItems++;

        itemsCollected.Add(itemID);
    }

   /* public void loadCoins()
    {
        //a list of all coins present in the scene
        CoinCollect[] allCoinsInScene = FindObjectsByType<CoinCollect>();

        if(coinsCollected != null)
        {
            //make our comparison 
            //increment through each coin present in the scene, and check if coinsCollected list has that coin in its list
            // if it does destroy it
            foreach(CoinCollect coin in allCoinsInScene)
            {
                if (coinsCollected.Contains(coin.coinID))
                {
                    Destroy(coin.gameObject);
                }
            }
        }

    }
    */

    //public void loadItems()
    //{
    //    questInteract questItem = FindFirstObjectByType<questInteract>();
    //    if (itemsCollected.Contains(questItem.itemID))
    //    {
    //        questItem.collection();
    //    }
    //}

    public void crateCollect()
    {
        Debug.Log("Collected a crate");
        totalCrates++;

        crateUpdate.Invoke(totalCrates);
    }


   public void sceneChangeCheck()
   {
        Debug.Log("You've changed scenes! Your current coin amount is : " + totalCoins.ToString());
   }

    public void movePlayerOnNPC(Transform newPos)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = newPos.position;

    }


}


[System.Serializable]

// adding a unity event!
public class addCoins : UnityEvent<int>
{



}

[System.Serializable]
public class addCrates : UnityEvent<int>
{

}
