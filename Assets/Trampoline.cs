using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private AudioSource audioSource;
    private Player player;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Player>(out player);
        if (player != null)
        {
            audioSource.Play();
            player.Rigidbody.AddForce(new Vector3(0, 30f, 0), ForceMode.Impulse);
        }

    }
}
