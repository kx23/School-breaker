using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private Text costText;
    [SerializeField] private int itemId = -1;
    [SerializeField] private Button button;

    public static UnityEvent OnShopUpdate = new UnityEvent();
    

    private void Awake()
    {
        button.onClick.AddListener(UpgradeItem);
        OnShopUpdate.AddListener(ShopUpdate);
        progressBar.Current = PlayerPrefs.GetInt(Inventory.itemsId[itemId]);
        ShopUpdate();
    }
    private void OnEnable()
    {
        ShopUpdate();
    }
    private void ShopUpdate()
    {
   
        button.interactable = !(PlayerPrefs.GetInt(Inventory.itemsId[itemId]) >= 5 || Inventory.ItemsImpCost[PlayerPrefs.GetInt(Inventory.itemsId[itemId])] > PlayerPrefs.GetInt("Coins"));

        if (PlayerPrefs.GetInt(Inventory.itemsId[itemId]) >= 5)
            costText.text = "MAX";
        else
            costText.text = "" + Inventory.ItemsImpCost[PlayerPrefs.GetInt(Inventory.itemsId[itemId])];
        
    }

    private void UpgradeItem() 
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - Inventory.ItemsImpCost[PlayerPrefs.GetInt(Inventory.itemsId[itemId])]);
        PlayerPrefs.SetInt(Inventory.itemsId[itemId], PlayerPrefs.GetInt(Inventory.itemsId[itemId]) + 1);
        OnShopUpdate.Invoke();
    }


}
