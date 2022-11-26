using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReseter : MonoBehaviour
{


    [SerializeField] private PlatformGenerator generator;
    Vector3 pos;
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.CompareTag("Platform")) return;
        if (collider.gameObject.GetComponent<Platform>() == null) return;

        Platform platformComponent = collider.gameObject.GetComponent<Platform>();
        collider.gameObject.SetActive(false);
        generator.ActivePlatforms.Remove(collider.gameObject);
        if (platformComponent.IsLastPlatform()) 
        {
            pos = collider.gameObject.transform.parent.gameObject.transform.position;
            pos.z += 360f;
            
            generator.Generate(pos);
            
            generator.AddToPool(collider.transform.parent.gameObject, platformComponent.GetId());
            collider.transform.parent.gameObject.SetActive(false);
            /*foreach (Transform child in collider.transform.parent.gameObject.transform)
                child.gameObject.SetActive(true);*/
        }
            


    }

}
