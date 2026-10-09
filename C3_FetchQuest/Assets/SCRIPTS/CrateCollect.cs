using UnityEngine;

public class CrateCollect : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            collection();
        }
    }



    public void collection()
    {
        GameController.instance.crateCollect();

        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<SphereCollider>().enabled = false;

    }

}
