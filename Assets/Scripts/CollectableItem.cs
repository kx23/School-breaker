using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : PickableItem
{
    int i = 0;
    bool jump = false;
    bool down = false;
    private Vector3 spawnPosition;
    [SerializeField] private bool withTint=false;
    [SerializeField] private GameObject vfx;
    private Animator animator;
    private Collider collider;
    private void Awake()
    {
        base.Awake();
        isSpin = false;
        spawnPosition = gameObject.transform.localPosition;
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }
    protected override void TriggerFunc(Collider other)
    {
        base.TriggerFunc(other);
        GameManager.OnGetCollectable.Invoke();
        if (withTint)
            DamageFade.OnGetCollectable.Invoke();

    }
    private void Start()
    {
    }
    private void Update()
    {
        //transform.localPosition = transform.localPosition+transform.up*0.1f;
    }
    private void OnEnable()
    {
        animator.Play("Idle");
        

        gameObject.transform.localPosition = spawnPosition;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3 (0,180f,0));
        collider.enabled = true;

        vfx.SetActive(Settings.IsVfxActive);
    }
    protected override IEnumerator SoundTimer()
    {
        audioSource.Play();
        animator.Play("Fly");
        vfx.SetActive(false);
        collider.enabled = false;
        float time = 0.0f;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(-35,Random.Range(90,270)));
        while (time<2f)
        {
            time += Time.deltaTime;
            transform.position = transform.position + transform.forward*40f *Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        collider.enabled = true;
        SetMeshStatus(true);
        gameObject.SetActive(false);
    }
    
}
