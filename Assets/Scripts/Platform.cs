using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    public static float speed = 20f;
    public static UnityEvent Change = new UnityEvent();

    [SerializeField] private GameObject ItemsContainer;
    [SerializeField] private GameObject ItemsContainerForCars;
    [SerializeField] private int id = 0;
    [SerializeField] private bool isEnd = false;
    [SerializeField] public GameObject obstacleContainer;
    [SerializeField] public GameObject obstacleContainerForCars;

    public int GetId()=>id;
    public bool IsLastPlatform() => isEnd;

    private void OnEnable()
    {
        SetActiveObstacles(true);
        SetActiveItems(true);
    }
    private void Awake()
    {
        Change.AddListener(() => ChangeObstaclesAndItems());
    }
    public void SetActiveObstacles(bool value) 
    {
        /*foreach (GameObject obstacle in obstacles) 
        {
            //obstacle.SetActive(value);
        }*/
        if (!GameManager.IsShip)
        {
            if (obstacleContainer != null)
                obstacleContainer.SetActive(true);
            if (obstacleContainerForCars != null)
                obstacleContainerForCars.SetActive(false);
            foreach (Transform child in obstacleContainer.transform)
            {
                child.gameObject.SetActive(value);

            }
        }
        else 
        {
            if (obstacleContainer != null)
                obstacleContainer.SetActive(false);
            if (obstacleContainerForCars != null)
                obstacleContainerForCars.SetActive(true);
            foreach (Transform child in obstacleContainerForCars.transform)
            {
                child.gameObject.SetActive(value);

            }
        }


    }
    public void SetActiveItems(bool value)
    {
        if (!GameManager.IsShip)
        {
            if (ItemsContainer != null)
                ItemsContainer.SetActive(true);
            if (ItemsContainerForCars != null)
                ItemsContainerForCars.SetActive(false);
            if (ItemsContainer != null)
            {
                ItemsContainer.SetActive(value);
            }
        }
        else
        {
            if(ItemsContainer!=null)
                ItemsContainer.SetActive(false);
            if (ItemsContainerForCars != null)
                ItemsContainerForCars.SetActive(true);
            if (ItemsContainerForCars != null)
            {
                ItemsContainerForCars.SetActive(value);
            }
        }


    }
    public void ChangeObstaclesAndItems()
    {
        SetActiveObstacles(true);
        SetActiveItems(true);
    }

}
