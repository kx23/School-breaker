using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObstacle : Obstacles
{
    [SerializeField] 
    protected float speed = 7f;
    protected bool isMoved = false;
    private Vector3 spawnPos = Vector3.zero;

    protected override void Update()
    {
        if(isMoved)
            transform.position += speed * Time.deltaTime * Vector3.back;
    }

    protected override void Start()
    {
        spawnPos = gameObject.transform.localPosition;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        isMoved = false;
        if(spawnPos != Vector3.zero)
            gameObject.transform.localPosition = spawnPos;
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {          
            TriggeredFunc();
        }
    }

    protected virtual void TriggeredFunc() => isMoved = false; 

    public virtual void StartMove() => isMoved = true;
}
