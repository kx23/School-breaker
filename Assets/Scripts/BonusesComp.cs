using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusesComp : MonoBehaviour
{

    private int magnetDur,
            blueBubbleDur,
            redBubbleDur;
    [SerializeField]
    private GameObject progressBarPanel;
    [SerializeField]
    private GameObject progressBarPrefab;
    private GameObject progressBarMagnet,
                       progressBarBubble;
    [SerializeField]
    private Sprite[] progressBarImages = new Sprite[3];
    private Coroutine bubbleShieldTimer,
                  bubbleShieldPBTimer,
                  magneticFieldTimer,
                  magneticFieldPBTimer;
    [SerializeField] private GameObject MagneticField;
    [SerializeField] public GameObject BubbleShield;
    // Start is called before the first frame update
    private void Awake()
    {
        magnetDur = 6 + PlayerPrefs.GetInt("Magnet") * 2;
        blueBubbleDur = 6 + PlayerPrefs.GetInt("BlueBubble") * 2;
        redBubbleDur = 6 + PlayerPrefs.GetInt("RedBubble") * 2;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMagneticField()
    {
        if (MagneticField.activeInHierarchy)
        {
            Destroy(progressBarMagnet);
            MagneticField.SetActive(false);
            StopCoroutine(magneticFieldTimer);
            StopCoroutine(magneticFieldPBTimer);
        }
        progressBarMagnet = Instantiate(progressBarPrefab, progressBarPanel.transform);
        progressBarMagnet.transform.GetChild(0).GetComponent<Image>().sprite = progressBarImages[0];
        magneticFieldTimer = StartCoroutine(MagneticFieldTimer());
    }
    public void ActivateBubbleShield(bool isBlue)
    {
        if (BubbleShield.activeInHierarchy)
        {
            Destroy(progressBarBubble);
            BubbleShield.SetActive(false);
            StopCoroutine(bubbleShieldTimer);
            StopCoroutine(bubbleShieldPBTimer);
        }
        progressBarBubble = Instantiate(progressBarPrefab, progressBarPanel.transform);
        if (isBlue)
            progressBarBubble.transform.GetChild(0).GetComponent<Image>().sprite = progressBarImages[1];
        else
            progressBarBubble.transform.GetChild(0).GetComponent<Image>().sprite = progressBarImages[2];
        bubbleShieldTimer = StartCoroutine(BubbleShieldTimer(isBlue));
    }
    IEnumerator progressBar(float dur, Slider pb)
    {
        float a = 1f / (dur - 1);
        a = a / 10f;
        while (true)
        {

            yield return new WaitForSeconds(0.1f);
            pb.value -= a;
        }
    }
    private IEnumerator BubbleShieldTimer(bool isBlue)
    {

        int dur;
        if (isBlue)
        {
            dur = blueBubbleDur;
            BubbleShield.tag = "BlueBubble";
        }
        else
        {
            dur = redBubbleDur;
            BubbleShield.tag = "Bubble";
        }

        bubbleShieldPBTimer = StartCoroutine(progressBar(dur, progressBarBubble.transform.GetChild(1).GetComponent<Slider>()));
        BubbleShield.SetActive(true);
        yield return new WaitForSeconds(dur);
        BubbleShield.SetActive(false);
        Destroy(progressBarBubble);
        StopCoroutine(bubbleShieldPBTimer);

    }
    private IEnumerator MagneticFieldTimer()
    {
        magneticFieldPBTimer = StartCoroutine(progressBar(magnetDur, progressBarMagnet.transform.GetChild(1).GetComponent<Slider>()));
        MagneticField.SetActive(true);
        yield return new WaitForSeconds(magnetDur);
        Destroy(progressBarMagnet);
        MagneticField.SetActive(false);
        StopCoroutine(magneticFieldPBTimer);

    }
    public void DeactivateShild()
    {
        Destroy(progressBarBubble);
        BubbleShield.SetActive(false);
        if (bubbleShieldTimer != null)
            StopCoroutine(bubbleShieldTimer);
        if (bubbleShieldPBTimer != null)
            StopCoroutine(bubbleShieldPBTimer);
    }
    public void DeactivateMagnet()
    {
        Destroy(progressBarMagnet);
        MagneticField.SetActive(false);
        if (magneticFieldTimer != null)
            StopCoroutine(magneticFieldTimer);
        if (magneticFieldPBTimer != null)
            StopCoroutine(magneticFieldPBTimer);
    }
}
