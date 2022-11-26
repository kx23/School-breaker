using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickableItem
{
    
    private Vector3 spawnPosition;
    private Transform _target;
    private bool isMagnetic = false;

    [SerializeField]
    private int initialAngle=0;
    protected override void Awake()
    {
        base.Awake();
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, initialAngle, transform.eulerAngles.z);
        spawnPosition = gameObject.transform.localPosition;
    }


    public void SetMagneticMove(Transform target) 
    {
        _target = target;
        isMagnetic = true;
    }

    private void Update()
    {
        if (!isMagnetic) return;
        Vector3 pos = _target.position;
        pos.z += 1.5f;
        pos.y += 1;
        transform.position = Vector3.Lerp(transform.position, pos, 15f * Time.deltaTime);
    }


    // Update is called once per frame
    protected override void OnEnable()
    {
        base.OnEnable();
        gameObject.transform.localPosition = spawnPosition;
    }


    protected override void TriggerFunc(Collider other)
    {
        base.TriggerFunc(other);
        GameManager.coins++;
        isMagnetic = false;
    }

}
