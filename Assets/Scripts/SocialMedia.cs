using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SocialMedia : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] Text rewardText;

	public string url;
	private bool rewarded = true;

    private void OnEnable()
    {
		if (PlayerPrefs.GetInt(gameObject.name) == 1)
			rewardText.text = "Done";
	}
    private void GetReward() 
	{
		if (PlayerPrefs.GetInt(gameObject.name)==1) return;

		PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coins")+1500);
		PlayerPrefs.SetInt(gameObject.name, 1);
		Application.OpenURL(url);
		rewardText.text="Done";
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		GetReward();
	}
}
