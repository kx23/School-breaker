using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    public static UnityEvent OnTutorialEnded = new UnityEvent();
    public static bool isTutorial = true;
    private bool tutorialStarted=false;
    private int slideNum = 0;
    [SerializeField] private GameObject container;
    [SerializeField] private Player player;
    private Animation arrowAnimation;

    // Start is called before the first frame update
    private void Awake()
    {

        isTutorial = PlayerPrefs.GetInt("Tutorial") == 0;
        gameObject.SetActive((PlayerPrefs.GetInt("Tutorial")==0)|| !(PlayerPrefs.HasKey("Tutorial")));
        //OnTutorialEnded?.RemoveAllListeners();
    }

    void Start()
    {
        player.OnDead.AddListener(() => gameObject.SetActive(false));
        arrowAnimation = container.GetComponentInChildren<Animation>();
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 0);

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (slideNum == 4)
            tutorialStarted = false;
        if (tutorialStarted)
        {
            if (slideNum == 0 && SwipeManager.IsSwipingRight())
                StartCoroutine("TimeOut");
            if (slideNum == 1 &&SwipeManager.IsSwipingUp())
                StartCoroutine("TimeOut");
            if (slideNum == 2 && SwipeManager.IsSwipingLeft())
                StartCoroutine("TimeOut");
            if (slideNum == 3 && SwipeManager.IsSwipingDown())
            {
                StartCoroutine("TimeOut");
                PlayerPrefs.SetInt("Tutorial", 1);
                OnTutorialEnded?.Invoke();
                isTutorial = false;
            }
                
        }


    }
    public IEnumerator TutorialStart()
    {
        
        yield return new WaitForSeconds(4);
        container.SetActive(true);

        tutorialStarted = true;

    }

    IEnumerator TimeOut()
    {

        container.SetActive(false);
        slideNum++;
        container.transform.eulerAngles = new Vector3(0, 0, slideNum * 90);
        yield return new WaitForSeconds(1);
        if (!(slideNum == 4))
            container.SetActive(true);
        else
            OnTutorialEnded?.Invoke();

    }
    public void StartTutorial() 
    {

        StartCoroutine("TutorialStart");
    }

}
