using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private bool isReload=false;
    private Vector3 bulletPos;
    [SerializeField] private GameObject bulletPrefab;
    private Coroutine reloadTimer;

    private void OnEnable()
    {
        isReload = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0|| Input.GetMouseButton(0))
            Shoot();

    }
    private void Shoot()
    {
        bulletPos = gameObject.transform.position;
        bulletPos.z += 5;
        bulletPos.y += 1f;
        if (isReload)
            return;
        isReload = true;
        Instantiate(bulletPrefab, bulletPos, new Quaternion(0, 0, 0, 0), transform.parent);
        reloadTimer = StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        
        yield return new WaitForSeconds(0.8f);
        isReload = false;
    }

}
