using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterCell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] int id = 0;
	[SerializeField] CharacterSelectorPage selector;

	public void OnPointerDown(PointerEventData eventData)
	{
		selector.SelectCharacter(id);
	}
}
