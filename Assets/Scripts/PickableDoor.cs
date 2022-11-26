using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableDoor : PickableItem
{
    // Start is called before the first frame update
    protected override void TriggerFunc(Collider other)
    {
        base.TriggerFunc(other);
        PlayerPrefs.SetInt("DoorsCount", PlayerPrefs.GetInt("DoorsCount")+1);
    }
}
