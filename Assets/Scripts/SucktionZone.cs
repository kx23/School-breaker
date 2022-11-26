using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SucktionZone : MonoBehaviour
{
    public GameObject blackHole;
    private GameObject player;


    void Update()
    {
        if (player != null)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, blackHole.transform.position, 6f * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Player")
        {
            player = other.gameObject;
            GameManager.OnStartLvlChange.Invoke();
            StartCoroutine(SucktionTimer());
        }
    }


    IEnumerator SucktionTimer()
    {
        yield return new WaitWhile(()=>Mathf.Abs(player.transform.position.z-blackHole.transform.position.z)>0.05f);
        GameManager.OnEndLvlChange.Invoke();
        player = null;
        transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
