using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationZone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(GameManager.IsShip)
                GameManager.OnPlayerTransformation.Invoke();
            GameManager.InPreparationZone = true;
        }
    }
}
