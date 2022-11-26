using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : PickableItem
{
    // Start is called before the first frame update


    protected override void TriggerFunc(Collider other)
    {
        int randomItemIndex = Random.Range(0, 3);
        base.TriggerFunc(other);
        switch (randomItemIndex)
        {
            case 0:
                other.gameObject.GetComponentInParent<BonusesComp>().ActivateMagneticField();
                break;
            case 1:
                other.gameObject.GetComponentInParent<BonusesComp>().ActivateBubbleShield(false);
                break;
            case 2:
                other.gameObject.GetComponentInParent<BonusesComp>().ActivateBubbleShield(true);
                break;

        }
    }
}
