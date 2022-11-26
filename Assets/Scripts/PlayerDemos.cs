using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemos : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Animator>().SetBool("isGrounded", true);
    }
    public void SetHighHurtBox()
    {
        
    }
}
