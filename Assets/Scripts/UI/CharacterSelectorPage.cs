using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectorPage : MonoBehaviour
{
    [SerializeField] Button acceptBtn;
    [SerializeField] Image[] borders;
    [SerializeField] List<GameObject> persons;
    private int characterIndex = 0;

    private void Awake()
    {
        acceptBtn.onClick.AddListener(Accept);
        foreach (GameObject person in persons) 
        {
            person.SetActive(true);
        }
    }

    private void Accept() 
    {
        PlayerPrefs.SetInt("skin_id", characterIndex);
        PlayerPrefs.Save();
        Input.ResetInputAxes();
        SceneManager.LoadScene(2);
        
    }

    public void SelectCharacter(int index) 
    {
        characterIndex = index;
        foreach(Image border in borders) 
        {
            border.enabled = false;
        }
        foreach (GameObject person in persons) 
        {
            person.GetComponent<Animator>().Play("Idle");
        }
        borders[index].enabled = true;
        persons[index].GetComponent<Animator>().Play("Run");
    }
}
