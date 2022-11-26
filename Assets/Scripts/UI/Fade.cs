using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public bool fadeStatus = false;
    private Image img;
    // Start is called before the first frame update
    private void Awake()
    {
        img = GetComponent<Image>();
    }
    void Start()
    {
        
        GameManager.OnStartLvlChange.AddListener(() => fadeStatus = true);
        GameManager.OnEndLvlChange.AddListener(() => fadeStatus = false);
    }

    // Update is called once per frame
    void Update()
    {
        FadeFunc();
    }

    public void FadeFunc()
    {
        if (fadeStatus)
        {
            if (img.color.a > 0.9f)
                img.color = new Color(0f, 0f, 0f, 1f);
            else
                img.color = Color.Lerp(img.color, new Color(0, 0, 0, 1), 6f *Time.deltaTime);
        }
        else
            if (img.color.a < 0.1f)
                img.color = new Color(0f, 0f, 0f, 0f);
            else
                img.color = Color.Lerp(img.color, new Color(0, 0, 0, 0), 6f * Time.deltaTime);




    }


}
