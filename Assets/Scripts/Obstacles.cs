using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private Collider obstacleCollider;
    [SerializeField] private bool catchBack = false;
    public bool CatchBack { get => catchBack; set => catchBack = value; }
    [SerializeField] private bool killWithoutCatching = false;
    public bool KillWithoutCatching { get => killWithoutCatching; set => killWithoutCatching = value; }

    private bool isBreak = false;
    private void Awake()
    {
        obstacleCollider = GetComponent<Collider>();
    }
    protected virtual void OnEnable()
    {
        isBreak = false;
        obstacleCollider.enabled = true;
    }

    public void Break() 
    {
        if(!isBreak) StartCoroutine(AnimateDestroy());

        isBreak = true;
    }

    protected virtual void Update() { }
    protected virtual void Start() { }

    private IEnumerator AnimateDestroy() 
    {
        obstacleCollider.enabled = false;
        bool isReduce = true;
        Vector3 spawnScale = transform.localScale;
        Vector3 minScale = spawnScale/2;

        //Два прохода: уменьшение в два раза, а потом увеличение до исходного размера
        for (int _ = 0; _ < 2; _++) { 
            for (float i = 0; i <= 1.1f; i += 0.2f)
            {
                gameObject.transform.localScale = isReduce ? Vector3.Lerp(spawnScale, minScale, i) : Vector3.Lerp(minScale, spawnScale, i);
                yield return new WaitForSeconds(0.02f);
            }
            isReduce = !isReduce;
        }

        gameObject.SetActive(false);
    }

}
