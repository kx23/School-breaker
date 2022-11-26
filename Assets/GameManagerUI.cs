using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerUI : MonoBehaviour
{

    [SerializeField]
    private Text scoreText, coinsText;
    [SerializeField] private Button carButton, motoButton;

    public Button MotoButton
    {
        get => motoButton;
    }
    public Button CarButton
    {
        get => carButton;
    }
    public Text ScoreText 
    {
        get => scoreText;
    }
    public Text CoinsText
    {
        get => coinsText;
    }

}
