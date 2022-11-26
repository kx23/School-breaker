using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Camera MainCamera { get => mainCamera; set => mainCamera = value; }
    public bool isFollow = true;
    [SerializeField] private Vector3 _distanceFromObject;
    private RaycastHit hitGround;
    private Collider platformCollider=null;
    private float yCoord= 0;
    private float yChangingSpeed = 10, xChangingSpeed = 10, zChangingSpeed = 15;

    private PlayerBase player;

    // Start is called before the first frame update
    void Start()
    {
        player= transform.gameObject.GetComponent<PlayerBase>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mainCamera == null || !isFollow)
            return;
        if ((platformCollider = GetFirstPlatformCollider()) != null)
        {
            yCoord = ((BoxCollider)platformCollider).size.y + ((BoxCollider)platformCollider).center.y;
        }
        if (player.GetGroundChecker.IsGrounded)
        {
            mainCamera.transform.position = new Vector3(transform.position.x + _distanceFromObject.x,
                                                    Mathf.Lerp(mainCamera.transform.position.y, transform.position.y + _distanceFromObject.y, Time.deltaTime * yChangingSpeed),
                                                    Mathf.Lerp(mainCamera.transform.position.z, transform.position.z + _distanceFromObject.z, Time.deltaTime * zChangingSpeed));

        } 
        else if (mainCamera.transform.position.y <= transform.position.y + 3f)
        {
            mainCamera.transform.position = new Vector3(transform.position.x + _distanceFromObject.x, transform.position.y + _distanceFromObject.y - 3f, transform.position.z + _distanceFromObject.z);
        }
        else if (Mathf.Abs(mainCamera.transform.position.y - transform.position.y) >= _distanceFromObject.y)
        {
            mainCamera.transform.position = new Vector3(transform.position.x + _distanceFromObject.x, transform.position.y + _distanceFromObject.y, transform.position.z + _distanceFromObject.z);
        }
        else
        {
            mainCamera.transform.position = new Vector3(transform.position.x + _distanceFromObject.x, mainCamera.transform.position.y, transform.position.z + _distanceFromObject.z);
        }
            


    }
    private Collider GetFirstPlatformCollider()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.TransformDirection(Vector3.down), out hitGround, 5, 1 << 7))
            if (hitGround.collider.gameObject.CompareTag("Platform"))
                return hitGround.collider;
        return null;
    }
}
