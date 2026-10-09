using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public string coinID;

    public void Awake()
    {
        //when obj is instantiated/loaded
        coinID = this.name + "_" + transform.position.ToString(); 
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            collection();
            Debug.Log(coinID + "  : collected");
        }
    }




    //this method is seperated from ontrigger in order to compartmentalize my code
    // in case i want to call this 'functional behavior' from another class

    /// <summary>
    /// Disable this gObj's components and invoke method from GameController.coinCollect
    /// </summary>
    public void collection()
    {
        //invokes a method in the GameController singleton
        // we use the 'instance' variable of that singleton to access the class data 
        //passing coinid as an arguement of coinCollect
        GameController.instance.coinCollect(coinID);

        //turn off componenets
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<SphereCollider>().enabled = false; 

    }





}
