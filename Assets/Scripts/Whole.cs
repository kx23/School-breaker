using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whole : Obstacles
{
    [SerializeField] private GameObject platform; 


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.transform.parent.GetComponent<Player>();
        player.GetComponent<CapsuleCollider>().enabled = false;
        PlatformMovement.isMove = false;
        player.GetFollowingCamera.isFollow = false;
        player.Rigidbody.AddForce(player.Rigidbody.velocity.x,-5f,20f,ForceMode.Impulse);
        Debug.Log("Отработало");
        
    }

    private void OnDisable()
    {
        platform.SetActive(true);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        platform.SetActive(false);
    }

}
