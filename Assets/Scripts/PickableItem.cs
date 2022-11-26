using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    protected AudioSource audioSource;
    [SerializeField] protected List<Renderer> meshRenderers;
    [SerializeField] protected bool isSpin=true;
    [SerializeField] protected float spinSpeed=1f;


    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeMeshList();

    }
    protected virtual void OnEnable()
    {
        GetComponent<Collider>().enabled = true;
        SetMeshStatus(true);
    }    

    private void InitializeMeshList() 
    {
        if (TryGetComponent(out MeshRenderer renderer))
            meshRenderers.Add(renderer);

        meshRenderers.AddRange(GetComponentsInChildren<MeshRenderer>());
        meshRenderers.AddRange(GetComponentsInChildren<SkinnedMeshRenderer>());
    }

    protected void SetMeshStatus(bool value) 
    {
        foreach (Renderer renderer in meshRenderers)
            renderer.enabled = value;
    }

    protected virtual IEnumerator SoundTimer()
    {
        SetMeshStatus(false);
        audioSource.Play();
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(audioSource.clip.length);
        gameObject.SetActive(false);

    }
    private void FixedUpdate()
    {
        if(isSpin)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + spinSpeed, transform.eulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerBase>().ItemGetted();
            TriggerFunc(other);
        }
    }

    protected virtual void TriggerFunc(Collider other) => StartCoroutine(SoundTimer()); 
}
