using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FPS : MonoBehaviour
{
    private Text fpsText;

    private void Awake()
    {
        fpsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void OnGUI()
    {
        fpsText.text = Convert.ToString((int)(1.0f / Time.deltaTime));
    }
}
