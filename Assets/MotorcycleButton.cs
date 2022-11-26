using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MotorcycleButton : ShipButton
{

     protected override void Update()
    {
        if (PlatformGenerator.currentId != 0)
        {
            GetComponent<Button>().interactable = false;
            return;
        }
        base.Update();
    }
}
