using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Menu) || Input.GetKeyDown(KeyCode.Home))
            {
                SetPause();
            }
        }
    }

    public void SetPause() 
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume() 
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }


}
