using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWhoopeeCushion : PickableItem
{
    // Start is called before the first frame update
    protected override void TriggerFunc(Collider other)
    {
        base.TriggerFunc(other);
        PlayerPrefs.SetInt("WhoopeeCushionsCount", PlayerPrefs.GetInt("WhoopeeCushionsCount") +1);
    }
}
