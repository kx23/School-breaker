using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    [SerializeField] Transform moneyDetectorTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
            other.GetComponent<Coin>().SetMagneticMove(moneyDetectorTransform);

    }
    // Start is called before the first frame update
}
