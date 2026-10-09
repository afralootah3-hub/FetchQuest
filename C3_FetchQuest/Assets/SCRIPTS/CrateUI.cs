using TMPro;
using UnityEngine;

public class CrateUI : MonoBehaviour
{
    public TextMeshProUGUI crateAmt;

    public void Start()
    {
        GameController.instance.crateInventory = this;
    }

    public void crateUpdate(int crates)
    {
        crateAmt.text = crates.ToString();
    }

}
