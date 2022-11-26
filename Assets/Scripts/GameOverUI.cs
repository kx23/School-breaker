using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Advertisements;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject doorButton, gloveButton;
    public bool IsCatched;
    public static UnityEvent OnGameOver = new UnityEvent();
    public Text Score;
    public Text Coins;
    private void Awake()
    {
        doorButton.GetComponent<Button>().onClick.AddListener(() => GameManager.OnRevival?.Invoke());
        doorButton.GetComponent<Button>().onClick.AddListener(() => PlayerPrefs.SetInt("DoorsCount", PlayerPrefs.GetInt("DoorsCount") - 1));
        gloveButton.GetComponent<Button>().onClick.AddListener(() => GameManager.OnRevival?.Invoke());
        gloveButton.GetComponent<Button>().onClick.AddListener(() => PlayerPrefs.SetInt("BoxingGlovesCount", PlayerPrefs.GetInt("BoxingGlovesCount") - 1));
    }
    public void SceneRepeat()
    {
        OnGameOver.Invoke();
        SceneManager.LoadScene("CharacterSelect");
    }

    public void MainMenu()
    {
        OnGameOver.Invoke();
        SceneManager.LoadScene("Main Menu");
    }
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("DoorsCount") > 0)
            doorButton.GetComponent<Button>().interactable = true;
        else
            doorButton.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("BoxingGlovesCount") > 0 &&IsCatched)
            gloveButton.GetComponent<Button>().interactable = true;
        else
            gloveButton.GetComponent<Button>().interactable = false;
    }
    


}
