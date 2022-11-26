using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private static float bulletSpeed;
    private int layerMask = 1 << 10;
    private RaycastHit hit;
    // need all hits to choose last one object was pierced
    private RaycastHit[] hits;

    private void Start()
    {
        GameManager.OnPlayerTransformation.AddListener(() => Destroy(gameObject));
        bulletSpeed = 3f;
        StartCoroutine(Clear());
    }
    IEnumerator Clear() 
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    
    private void FixedUpdate()
    {

        gameObject.transform.position += Vector3.forward * bulletSpeed;
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * bulletSpeed, Color.yellow);
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.back), bulletSpeed, layerMask);
        if (hits.Length!=0)
            {
            hit = hits[hits.Length - 1];
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Obstacles"))
            {
                hit.collider.gameObject.GetComponent<Obstacles>().Break();
            }
            Destroy(gameObject);
        }            
    }

}
