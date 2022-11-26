using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundChecker),typeof(FollowingCamera))]
public class PlayerBase : MonoBehaviour
{
    [SerializeField] private GameObject fx;
    
    private GroundChecker groundChecker;
    public GroundChecker GetGroundChecker { get => groundChecker; }

    private FollowingCamera followingCamera;
    public FollowingCamera GetFollowingCamera { get => followingCamera; }
    [SerializeField] public BoxCollider HurtBox;
    // Start is called before the first frame update

    protected virtual void Awake()
    {
        groundChecker = GetComponent<GroundChecker>();
        followingCamera = GetComponent<FollowingCamera>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Move()
    { }

    public void ItemGetted()
    {
        if (!Settings.IsVfxActive) return;

        if (fx.activeSelf)
            fx.SetActive(false);
        fx.SetActive(true);
    }

}
