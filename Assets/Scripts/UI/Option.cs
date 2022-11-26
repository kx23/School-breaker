using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Option : MonoBehaviour
{

    [SerializeField] private Button optionBtn;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle vfxToggle;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("mute")) 
        {
            PlayerPrefs.SetInt("mute", 0);
        }

        if (!PlayerPrefs.HasKey("vfx"))
        {
            PlayerPrefs.SetInt("vfx", 1);
        }

        Settings.IsMuted = Convert.ToBoolean(PlayerPrefs.GetInt("mute"));
        Settings.IsVfxActive = Convert.ToBoolean(PlayerPrefs.GetInt("vfx"));

    }

    void Start()
    {
        optionBtn.onClick.AddListener(()=>optionPanel.SetActive(!optionPanel.activeSelf));

        soundToggle.isOn = !Convert.ToBoolean(PlayerPrefs.GetInt("mute"));
        vfxToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("vfx"));

        soundToggle.onValueChanged.AddListener((bool value) => Settings.IsMuted = !value);
        vfxToggle.onValueChanged.AddListener((bool value) => Settings.IsVfxActive = value);
    }

}
