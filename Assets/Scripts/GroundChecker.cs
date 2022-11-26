using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private bool isGrounded;
    private RaycastHit hitGround;
    private GameObject stayPlatform;
    public bool IsGrounded { get => isGrounded; }
    public void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.TransformDirection(Vector3.down), out hitGround, 1.5f, 1 << 7);
    }

    public void FindCurrentPlatform()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.down), 30f, 1 << 7);
        Platform movement;

        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.gameObject.TryGetComponent<Platform>(out movement))
                continue;
            stayPlatform = movement.gameObject;
            break;
        }

        

        if (stayPlatform.GetComponent<Platform>().GetId() == PlatformGenerator.currentId)
            GameManager.OnEnviromentChange?.Invoke();
        Debug.Log(stayPlatform.name);
        if (stayPlatform == null) return;
    }

    public Platform GetCurrentPlatform() => stayPlatform.GetComponent<Platform>();
}
