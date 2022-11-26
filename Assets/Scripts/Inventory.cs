using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    public static Dictionary<string, int> items = new Dictionary<string, int>(){// название, цена
        {"WhoopeeCushion",500},
        {"Door",2000},
        {"BoxingGlove",1500 },
        {"Vehicle",1000 }
        
    };

    public static bool InStock(string itemName) => items[itemName] > 0;

    public static void AddToItem(string itemName)
    {
        PlayerPrefs.SetInt(itemName+"sCount", PlayerPrefs.GetInt(itemName + "sCount") + 1);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - items[itemName]);
    }

    public static Dictionary<int,string> itemsId = new Dictionary<int, string> (){
        
        {0,"Magnet"},
        {1, "BlueBubble"},
        {2, "RedBubble" },
        {3, "VehicleDuration" },
        {4, "VehicleAppears" }


    };
    public static int[] ItemsImpCost = { 500, 1000, 3000, 10000, 50000 };//цены улучшений

}
