using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    private static float raycastLength=40;
    private int layerMask = 1 << 9;
    private RaycastHit hit;
    // need all hits to choose last one object was pierced
    private RaycastHit[] hits;
    [SerializeField] private Material [] Materials;
    private int obstacles;
    public void DecreaseObstacles(int value)
    {
        obstacles = obstacles - value;
        if (obstacles == 0)
            gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (gameObject.CompareTag("BlueBubble"))
        {
            gameObject.GetComponent<MeshRenderer>().material= Materials[0];
            obstacles = 1;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = Materials[1];
            obstacles = 2;
        }
            

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.CompareTag("Obstacles") || obstacles <= 0)
            return;

        other.gameObject.SetActive(false);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastLength, Color.yellow);
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), raycastLength, layerMask);
        if (hits.Length != 0)
        {
            foreach (RaycastHit hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Obstacles"))
                {
                    hit.collider.gameObject.SetActive(false);
                }
            }

        }
        obstacles--;
        if (obstacles == 0)
        {
            gameObject.transform.parent.gameObject.GetComponent<BonusesComp>().DeactivateShild();
        }
    }
}
