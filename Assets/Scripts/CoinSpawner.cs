using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CoinSpawner : MonoBehaviour
{
    [System.Serializable]
    private class CoinLine
    {
        public List<GameObject> coinLine;
    }

    [SerializeField] List<CoinLine> coinsPrefs = new List<CoinLine>();
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnDisable()
    {
        foreach (CoinLine elem in coinsPrefs)
        {
            foreach (GameObject element in elem.coinLine)
            {
                element.SetActive(false);
            }

        }
    }
    private void OnEnable()
    {
        int rndIndex = Random.Range(0, coinsPrefs.Count);
        
        foreach (GameObject element in coinsPrefs[rndIndex].coinLine)
        {
            element.SetActive(true);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
