using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CoinsText : MonoBehaviour
{
    // Start is called before the first frame update
    private Text coinText;
    private void Awake()
    {
        coinText = GetComponent<Text>();
        ShopItem.OnShopUpdate.AddListener(() => coinText.text = "" + PlayerPrefs.GetInt("Coins"));
        VehicleCountUI.OnShopUpdate.AddListener(() => coinText.text = "" + PlayerPrefs.GetInt("Coins"));
    }
    private void OnEnable()
    {
        coinText.text = "" + PlayerPrefs.GetInt("Coins");
    }
}
