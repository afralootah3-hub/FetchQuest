using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinAmt;

    public void Awake()
    {
        GameController.instance.loadCoinUI(this);
    }

    public void coinUpdate(int coins)
    { 
        coinAmt.text = coins.ToString();
    }



}
