using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipButton : MonoBehaviour
{
    private bool isVehicle=false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameManager.OnCarTransformation.AddListener(() => isVehicle = true);
        GameManager.OnPlayerTransformation.AddListener(() => isVehicle = false);
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (Tutorial.isTutorial || /*GameManager.IsShip*/ isVehicle||GameManager.InPreparationZone||GameManager.isCD||!GameManager.HaveCar)
            GetComponent<Button>().interactable = false;
        else
            GetComponent<Button>().interactable = true;
    }
}
