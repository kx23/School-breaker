using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstEntry"))
        {
            PlayerPrefs.SetInt("RedBubble", 0);
            PlayerPrefs.SetInt("BlueBubble", 0);
            PlayerPrefs.SetInt("Magnet", 0);
            PlayerPrefs.SetInt("VehicleDuration", 0);
            PlayerPrefs.SetInt("VehicleAppears", 0);
            PlayerPrefs.SetInt("Coins", 100000);
            PlayerPrefs.SetInt("DoorsCount", 0);
            PlayerPrefs.SetInt("BoxingGlovesCount", 0);
            PlayerPrefs.SetInt("VehiclesCount", 0);
            PlayerPrefs.SetInt("WhoopeeCushionsCount", 0);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("Tutorial", 0);
            PlayerPrefs.SetInt("Youtube", 0);
            PlayerPrefs.SetInt("Reddit", 0);
            PlayerPrefs.SetInt("Twitter", 0);
            PlayerPrefs.SetInt("Facebook", 0);
            PlayerPrefs.SetInt("Instagram", 0);
            PlayerPrefs.SetInt("FirstEntry", 1);
        }
        Debug.Log(PlayerPrefs.GetInt("Coins"));
    }
}
