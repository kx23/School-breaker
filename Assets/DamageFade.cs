using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DamageFade : MonoBehaviour
{
    //public static UnityEvent OnGetCollectable = new UnityEvent();

    public static UnityEvent OnGetCollectable = new UnityEvent();
    private Image img;
    public bool fadeStatus = false;
    private Coroutine fadeCoroutine;
    float speed = 0.02f;
    float incr = -1;
    private void Awake()
    {
        img = GetComponent<Image>();
        OnGetCollectable.AddListener(() => StartFading());
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeStatus)
        {
            if (img.color.a >= 1)
                incr = -1;
            else if (img.color.a < 0.4)
                incr = 1;
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + incr*speed);
            return;
        }
        if(img.color.a>0)
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a  -speed);
    }
    private void StartFading()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTimer());
    }

    IEnumerator FadeTimer()
    {
        fadeStatus = true;
        yield return new WaitForSeconds(3f);
        fadeStatus = false;
    }


}
