using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformsRemover : MonoBehaviour
{
    [SerializeField] List<GameObject> platforms = new List<GameObject>();
    private void Start()
    {
        //GameManager.OnCarTransformation.AddListener(()=>se)
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        SetPlatforms();
    }
    public void SetPlatforms()
    {
        foreach (GameObject item in platforms)
        {
            item.SetActive(!GameManager.IsShip);
        }
    }
}
