using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedCoinLine : CoinLine
{
    [SerializeField]
    private List<GameObject> CurvedLinesPrefabs = new List<GameObject>();
    override protected void OnEnable()
    {
        CurvedLinesPrefabs[(((int)(PlatformMovement.speed-20))/5)].SetActive(true);
    }
    private void OnDisable()
    {
        foreach (GameObject curvedLine in CurvedLinesPrefabs)
            curvedLine.SetActive(false);
    }
}
