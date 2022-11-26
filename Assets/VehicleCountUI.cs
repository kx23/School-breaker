using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class VehicleCountUI : MonoBehaviour
{
    [SerializeField]
    private Text text,costText;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private Button button;

    public static UnityEvent OnShopUpdate = new UnityEvent();
    void Start()
    {
    }
    private void Awake()
    {
        OnShopUpdate.AddListener(ShopUpdate);
        costText.text = "" + Inventory.items[itemName];
    }
    private void OnEnable()
    {
        ShopUpdate();
    }
    public void AddItem() 
    {
        Inventory.AddToItem(itemName);
        Debug.Log(PlayerPrefs.GetInt("Coins"));
        OnShopUpdate.Invoke();
    }
    private void ShopUpdate()
    {
        text.text = PlayerPrefs.GetInt(itemName + "sCount").ToString() + "x";
        if (Inventory.items[itemName] > PlayerPrefs.GetInt("Coins"))
            button.interactable = false;
        else
            button.interactable = true;
    }
}
