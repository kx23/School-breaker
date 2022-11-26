using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    //Добавление прогресс бара через редактор Unity
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar() 
    {
        GameObject inst = Instantiate(Resources.Load<GameObject>("UI/ProgressBar"));
        inst.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    [SerializeField]
    private int minimum,
                maximum,
                current;
     
    public int Current {

        get => current;
        set => current = Current < maximum ? value : maximum;
    }

    [SerializeField]
    private Image mask, 
                  fill;

    [SerializeField]
    private Color color;

    private void Update()
    {
        GetCurrentFill();
        Recolor(color);
    }

    public void GetCurrentFill() 
    {
        float currentOffset = Current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }

    private void Recolor(Color newColor) 
    {
        fill.color = newColor;
    }

    public void AddValue(int value) 
    {
        Current+=value;
        Debug.Log(PlayerPrefs.GetInt("Coins"));
    }

}
