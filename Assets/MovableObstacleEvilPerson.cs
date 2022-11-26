using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObstacleEvilPerson : MovableObstacle
{
    private float maxSpeed = 12f;
    float currentSpeed = 0;
    Coroutine coroutine;

    protected override void OnEnable()
    {
        GetComponent<Animator>().Play("Idle");
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
        currentSpeed = speed;
    }

    protected override void Update()
    {
        if (isMoved)
            transform.position += currentSpeed * Time.deltaTime * Vector3.back;
    }

    public void SetHighHurtBox() { }

    public override void StartMove()
    {
        base.StartMove();
        GetComponent<Animator>().Play("Run");

        if(coroutine!=null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(SpeedControler());
    }

    protected override void TriggeredFunc()
    {
        base.TriggeredFunc();
        GetComponent<Animator>().Play("Catch");
    }

    private IEnumerator SpeedControler() 
    {
        for (currentSpeed = speed; currentSpeed < maxSpeed; currentSpeed += 0.5f) 
        {
            yield return new WaitForSeconds(.2f);
        }
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);

        if (collision.gameObject.CompareTag("Obstacles")) 
        {
            GetComponent<Animator>().Play("Idle");
            isMoved = false;
        }
    }
}
