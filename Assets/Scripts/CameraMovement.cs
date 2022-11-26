using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public FollowingCamera player;
    private Animation animationComponent;
    public Animation GetAnimation { get => animationComponent; }
    private Camera cameraComponent;
    public Camera GetCamera { get => cameraComponent; } 
    private void Awake()
    {
        animationComponent = GetComponent<Animation>();
        cameraComponent = GetComponent<Camera>();
    }
    public void ReadyToStart()
    {
        GameManager.OnStartRun?.Invoke();
    }
    public void AnchorCamera()
    {
        player.MainCamera = gameObject.GetComponent<Camera>();
    }
    private void Update()
    {
        if (player == null)
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 6, -90), Time.deltaTime * 5);
    }

}
