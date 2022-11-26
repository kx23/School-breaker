using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : PickableItem
{
    protected override void TriggerFunc(Collider other)
    {
        base.TriggerFunc(other);
        other.gameObject.GetComponentInParent<BonusesComp>().ActivateBubbleShield(gameObject.CompareTag("BlueBubble"));
    }
}
