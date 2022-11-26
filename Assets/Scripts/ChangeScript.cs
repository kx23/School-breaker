using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScript : MonoBehaviour
{
    GameObject curSkin;
    int cur, 
        next;
    bool isChanging;
    [SerializeField] GameObject [] Skins;
    [SerializeField] GameObject player;

    public void Change()
    {
        if (isChanging) return;
        do
        {
            next = Random.Range(0, Skins.Length);
        }
        while (next == cur);
        Destroy(curSkin);
        curSkin = Instantiate(Skins[cur = next], player.transform);
        
        StartCoroutine(AnimatorReset());
    }
    private void OnEnable()
    {
        curSkin = Instantiate(Skins[cur = Random.Range(0, Skins.Length)], player.transform);
        StartCoroutine(AnimatorReset());

    }
    IEnumerator AnimatorReset()
    {
        
        yield return 0;
        player.GetComponent<Animator>().Rebind();
        isChanging = true;
        yield return 0;
        isChanging = false;
    }
}
