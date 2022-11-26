using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameSelectUI : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private InputField inputText;
    [SerializeField] private GameObject characterSelector;

    void Start()
    {
        if (PlayerPrefs.HasKey("nickname")) GoToPageCharacterSelect();

        btn.onClick.AddListener(Save);
    }

    private void Save() 
    {
        if (inputText.text == "") return;

        PlayerPrefs.SetString("nickname", inputText.text);
        GoToPageCharacterSelect();
    }

    private void GoToPageCharacterSelect() 
    {
        characterSelector.SetActive(true);
        gameObject.SetActive(false);
    }

}
