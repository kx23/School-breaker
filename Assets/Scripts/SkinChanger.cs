using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] skins;
    bool isFirst = true;
    private GameObject curSkin;

    private void OnEnable()
    {
        if (isFirst)
        {
            Change(PlayerPrefs.GetInt("skin_id", 0));
            isFirst = false;
        }
           
    }


    public void Change(int index) 
    {
        curSkin = Instantiate(skins[index], this.gameObject.transform.position +new Vector3(0.1f,0,0),this.gameObject.transform.rotation, this.gameObject.transform);
        GetComponent<Animator>().Rebind();
    }

}
