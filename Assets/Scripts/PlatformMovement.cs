using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public static bool isMove = false;
    public static float speed=20;


    // Update is called once per frame
    void Update()
    {
        if (!isMove) return;

        transform.position += speed * Time.deltaTime * Vector3.back;
    }
}
