using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ship : PlayerBase
{
    public bool IsMotorcycle;
    private float rotationSpeed = 15f;
    private float speed = 11f;
    float x;
    Ray ray;
    RaycastHit hit;
    Vector3 playerVector, cursor;
    //[SerializeField] private Animation motorcycleAnimation;
    private GameObject activeSkin;
    [SerializeField] GameObject[] Skins = new GameObject[3];
    [SerializeField] GameObject MotorcycleSkin;


    void Update()
    {
        //activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 20, 0)), 0.1f);
        Move();
    }

    private void OnEnable()
    {
        //StartCoroutine(Transformation());
        Debug.Log(IsMotorcycle);
        if (!IsMotorcycle)
            activeSkin = Skins[PlatformGenerator.currentId];
        else
            activeSkin = MotorcycleSkin;
        activeSkin.SetActive(true);
    }
    private void OnDisable()
    {
        foreach (GameObject a in Skins)
        {
            a.SetActive(false);
            a.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        MotorcycleSkin.SetActive(false);
            
    }
    protected override void Move()
    {
        x = 0;
        playerVector = gameObject.transform.position;
#if (UNITY_ANDROID||UNITY_IOS)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit))
            {
                x = hit.point.x;

            }
            if (Mathf.Abs(x - playerVector.x) > 0.5)
            {

                if (x < -3.81f) x = -3.81f;
                if (x > 3.81f) x = 3.81f;
                cursor = new Vector3(x, playerVector.y, playerVector.z);
                gameObject.transform.position = Vector3.MoveTowards(playerVector, cursor, speed * Time.deltaTime);
                if (transform.position.x > -3.81f && transform.position.x < 3.81f)
                {
                    if (playerVector.x < cursor.x)
                    {
                        if (IsMotorcycle)
                        {
                            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 0, -30)), rotationSpeed * Time.deltaTime);
                        }
                        else
                            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 20, 0)), rotationSpeed * Time.deltaTime);
                    }
                    if (playerVector.x > cursor.x)
                    {
                        if (IsMotorcycle)
                        {
                            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 30)), rotationSpeed * Time.deltaTime);
                        }
                        else
                            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, -20, 0)), rotationSpeed * Time.deltaTime);
                    }

                }
                else
                    activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), rotationSpeed * Time.deltaTime);

            }
            else
                activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), rotationSpeed * Time.deltaTime);

        }
        else
            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), rotationSpeed * Time.deltaTime);
#endif
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                x = hit.point.x;
            }
            if (Mathf.Abs(x - playerVector.x) > 0.5)
            {

                if (x < -3.81f) x = -3.81f;
                if (x > 3.81f) x = 3.81f;
                cursor = new Vector3(x, playerVector.y, playerVector.z);
                gameObject.transform.position = Vector3.MoveTowards(playerVector, cursor, speed * Time.deltaTime);
                if (transform.position.x > -3.81f && transform.position.x < 3.81f)
                {
                    if (playerVector.x < cursor.x)
                    {
                        if (IsMotorcycle)
                        {
                            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 0, -30)), rotationSpeed * Time.deltaTime);
                        }
                        else
                            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 20, 0)), rotationSpeed * Time.deltaTime);
                    }
                    if (playerVector.x > cursor.x)
                    {
                        if (IsMotorcycle)
                        {
                            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 30)), rotationSpeed * Time.deltaTime);
                        }
                        else
                            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, -20, 0)), rotationSpeed * Time.deltaTime);
                    }

                }
                else
                    activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), rotationSpeed * Time.deltaTime);

            }
            

        }
        else
            activeSkin.transform.rotation = Quaternion.Lerp(activeSkin.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), rotationSpeed * Time.deltaTime);
#endif
    }

}
