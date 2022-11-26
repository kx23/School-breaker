using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableBoxingGlove : PickableItem
{
    // Start is called before the first frame update
    protected override void TriggerFunc(Collider other)
    {
        base.TriggerFunc(other);
        PlayerPrefs.SetInt("BoxingGlovesCount", PlayerPrefs.GetInt("BoxingGlovesCount") +1);
    }
}
