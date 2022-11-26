using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameManagerUI managerUI;
    private Score score;
    public static UnityEvent OnGetCollectable = new UnityEvent();
    public static UnityEvent OnVehicle=new UnityEvent();
    public static UnityEvent Player = new UnityEvent();

    [SerializeField]
    private float swapSpeed = 0.8f;
    private int vehicleDur,
                vehicleCDDur;
    public static bool IsShip = false, isCD=false,HaveCar=false;
    public static bool InPreparationZone = false;
    public static UnityEvent OnEnviromentChange = new UnityEvent();
    private const int changeDist = 5000;
    [SerializeField] private Player player;
    [SerializeField] private Ship ship;
    [SerializeField] private CameraMovement mainCamera;
    private EvilPerson evilPersonScript;
    [SerializeField]
    private GameObject evilPerson,
                                                 gameOverPanel,
                                                 tutorial,
                                                 boomEffect,
                                                 magicPoofEffect;
    [SerializeField] private PlatformGenerator generator;
    [SerializeField] private Material[] skyboxes = new Material[3];
    [SerializeField] private Color[] colors = new Color[3];
    
    public static UnityEvent OnStartLvlChange = new UnityEvent(),
                             OnEndLvlChange = new UnityEvent(),
                             OnStartRun = new UnityEvent(),
                             OnRevival = new UnityEvent(),
                             OnCarTransformation=new UnityEvent(),
                             OnPlayerTransformation = new UnityEvent();
                                
    private int maxSpeed=40;
    private float curSpeed;
    private int limitToChangeScore = 500;
    public static int coins = 0;
    private Coroutine carCoroutine, shipForwardCor,playerBackCor, carCDCor, carPBTimer;
    private int targetScoreChange = 0;
    [SerializeField]
    private GameObject progressBarPrefab;
    private GameObject progressBarVehicle;
    [SerializeField]
    private GameObject progressBarPanel;
    [SerializeField]
    private Sprite progressBarImage;
    private void Awake()
    {
        ClearEventsListeners();
        score = GetComponent<Score>();
        OnGetCollectable.RemoveAllListeners();
        isCD = false;
        HaveCar = (PlayerPrefs.GetInt("VehiclesCount") > 0);
        
        evilPersonScript = evilPerson.GetComponent<EvilPerson>();
        GameOverUI.OnGameOver.AddListener(()=>PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coins));
        GameOverUI.OnGameOver.AddListener(() => coins=0);
        vehicleDur = 10 + PlayerPrefs.GetInt("VehicleDuration") * 2;
        vehicleCDDur = 30 - PlayerPrefs.GetInt("VehicleAppears") * 2;

    }



    private void Start()
    {

        Application.targetFrameRate = 60;
        
        player.OnDead.AddListener(() => {
            Result();
            managerUI.CarButton.gameObject.SetActive(false); 
            managerUI.MotoButton.gameObject.SetActive(false);
        });

        targetScoreChange = 4200;
        OnStartLvlChange.AddListener(()=>SetMovement(false));
        OnEndLvlChange.AddListener(ChangeLevel);
        OnEnviromentChange.RemoveAllListeners();

        player.OnCatchedBack.AddListener(() => { 
            evilPersonScript.StopCoroutine("pursuitTimer");
            evilPerson.SetActive(true);
            evilPersonScript.StartPursuit();
        });

        OnRevival.AddListener(() => {
            Revival();
            gameOverPanel.SetActive(false);
            evilPerson.SetActive(false);
            player.Animator.Play("Run");
            managerUI.CarButton.gameObject.SetActive(true);
            managerUI.MotoButton.gameObject.SetActive(true);
        });

        OnCarTransformation.AddListener(() => TranformationToShip());
        OnPlayerTransformation.AddListener(() => TranformationToPlayer());
        OnStartRun.AddListener(()=> {
            player.movable = true;
            PlatformMovement.isMove = true;
            managerUI.CarButton.gameObject.SetActive(true);
            managerUI.MotoButton.gameObject.SetActive(true);

        });
        
        PlatformMovement.speed = 20;

    }

    public void OnSmash()
    {
        if (evilPerson.activeInHierarchy)
        {

            if (player.GetBonusesComp.BubbleShield.activeInHierarchy)
                player.GetBonusesComp.BubbleShield.GetComponent<BubbleShield>().DecreaseObstacles(1);
            else
            {
                player.OnCatchedBack?.Invoke();
                player.OnDead?.Invoke();
            }
                
            return;
        }
        else 
            evilPerson.SetActive(true);
    }

    IEnumerator progressBar(float dur, Slider pb)
    {
        float a = 1f / (dur - 1);
        a = a / 10f;
        while (true)
        {

            yield return new WaitForSeconds(0.1f);
            pb.value -= a;
        }
    }
    void FixedUpdate()
    {
        player.Animator.SetFloat("runMultiplayer", PlatformMovement.speed / 20f);
        if ((score.GetScore > limitToChangeScore)&&(maxSpeed>PlatformMovement.speed))
            ChangePlatformMovementSpeed();
        managerUI.CoinsText.text = string.Format("{0}", coins);
        managerUI.ScoreText.text = string.Format("{0}", (int)score.GetScore);
        if (player.isDead||!PlatformMovement.isMove) return;
        
        if (score.GetScore > targetScoreChange) 
        { 
            PlatformGenerator.nextChanging = true;
            targetScoreChange = (int)score.GetScore + changeDist;
        }
    }

    private void Result()
    {
        GameOverUI gameOverUI = gameOverPanel.GetComponent<GameOverUI>();
        string scoreText;
        if (score.GetScore > PlayerPrefs.GetInt("Score"))
        {
            score.SetBestScore();
            scoreText = string.Format("New Best Score: {0}", (int)score.GetScore);
        }
        else
        {
            scoreText = string.Format("Best Score: {0} \nCurrent score: {1}", PlayerPrefs.GetInt("Score"), (int)score.GetScore);
        }
        
        gameOverUI.Score.text = scoreText;
        gameOverUI.Coins.text = "Coins: " + coins;
        gameOverUI.IsCatched=player.IsCatched;
        gameOverPanel.SetActive(true);
    }

    void ChangeLevel()
    {
        generator.ClearActivePlatforms();
        int rndIndex;
        do
        {
            rndIndex = Random.Range(0, generator.GetSpawnPoolLenght());
        }
        while (rndIndex == PlatformGenerator.currentId);
        PlatformGenerator.currentId = rndIndex;
        generator.GenerateInitialPath();
        player.transform.position = new Vector3(0, 0.1f, -80);
        RenderSettings.skybox = skyboxes[PlatformGenerator.currentId];
        RenderSettings.fogColor = colors[PlatformGenerator.currentId];
        StartCoroutine(StartNewLvl());
    }

    private void SetMovement(bool status) 
    {
        player.playerMovement.ToMiddlePos();
        PlatformMovement.isMove = status;
        player.Immobolize(!status);
    }

    private void ChangePlatformMovementSpeed()
    {
        PlatformMovement.speed += 0.2f;
        limitToChangeScore += 500;
    }
    IEnumerator StartNewLvl()
    {
        yield return new WaitForSeconds(0.5f);
        SetMovement(true);
        InPreparationZone = false;
    }

    public void StartClick()
    {
        player.Animator.SetTrigger("Looking");
        mainCamera.GetAnimation.Play();

        evilPerson.SetActive(true);
        if(tutorial.activeInHierarchy)
            tutorial.GetComponent<Tutorial>().StartTutorial();

    }
    private void Revival()
    {

        player.GetGroundChecker.GetCurrentPlatform().SetActiveObstacles(false);
        generator.ActivePlatforms[generator.ActivePlatforms.IndexOf(player.GetGroundChecker.GetCurrentPlatform().gameObject) + 1].GetComponent<Platform>().SetActiveObstacles(false);
        player.isDead = false;
        player.IsCatched = false;
        if (player.GetGroundChecker.GetCurrentPlatform().GetId() == 2) 
        {
            player.GetComponent<CapsuleCollider>().enabled = true;
            player.transform.position = new Vector3(
                player.transform.position.x,
                0.6f,
                -80f
                );
            player.GetFollowingCamera.isFollow = true;
        }
        PlatformMovement.isMove = true;
    }
    public void Trasformation()
    {
        OnCarTransformation.Invoke();
    }
    public void VehicleButtonClick(bool isMotorcycle)
    {
        ship.GetComponent<Ship>().IsMotorcycle = isMotorcycle;
        Trasformation();
        PlayerPrefs.SetInt("VehiclesCount", PlayerPrefs.GetInt("VehiclesCount") - 1);
        HaveCar = (PlayerPrefs.GetInt("VehiclesCount") > 0);

    }
    private IEnumerator CarTimer()
    {
        progressBarVehicle = Instantiate(progressBarPrefab, progressBarPanel.transform);
        progressBarVehicle.transform.GetChild(0).GetComponent<Image>().sprite = progressBarImage;
        carPBTimer = StartCoroutine(progressBar(vehicleDur, progressBarVehicle.transform.GetChild(1).GetComponent<Slider>()));
        IsShip = true;
        yield return new WaitForSeconds(vehicleDur);
        IsShip = false;
        OnPlayerTransformation.Invoke();
    }

    IEnumerator CameraTimer()
    {
        yield return new WaitForSeconds(0.5f);
        mainCamera.player = player.GetComponent<FollowingCamera>();
    }
    private void TranformationToShip() 
    {
        mainCamera.player = null;
        playerBackCor = StartCoroutine(ObjectBack(player,true));
        shipForwardCor = StartCoroutine(ObjectForward(ship));
        carCoroutine = StartCoroutine(CarTimer());
        curSpeed = PlatformMovement.speed;
        PlatformMovement.speed = maxSpeed;
        generator.SwitchObstaclesAndItems();
        if (magicPoofEffect.activeSelf)
            magicPoofEffect.SetActive(false);
        magicPoofEffect.SetActive(true);

    }
    public void TranformationToPlayer()
    {
        if (IsShip)
        {
            StopCoroutine(carCoroutine);
        }
        StopCoroutine(shipForwardCor);
        StopCoroutine(playerBackCor);
        Destroy(progressBarVehicle);
        StopCoroutine(carPBTimer);
        mainCamera.player = null;
        StartCoroutine(ObjectBack(ship));
        StartCoroutine(ObjectForward(player,true));
        carCDCor = StartCoroutine(CarCDTimer());
        IsShip = false;
        PlatformMovement.speed = curSpeed;
        generator.SwitchObstaclesAndItems();
        if (boomEffect.activeSelf)
            boomEffect.SetActive(false);
        boomEffect.SetActive(true);
    }

    /*private void TransformationTo(PlayerBase from, PlayerBase to, float newSpeed, GameObject vfxEffect) 
    {
        mainCamera.player = null;
        StartCoroutine(ObjectBack(ship));
        StartCoroutine(ObjectForward(player, true));
        PlatformMovement.speed = curSpeed;
        generator.SwitchObstaclesAndItems();

        if (boomEffect.activeSelf)
            boomEffect.SetActive(false);
        boomEffect.SetActive(true);
    }*/

    private IEnumerator ObjectBack(PlayerBase movableObject, bool isPlayer = false) 
    {
        if (isPlayer) 
        {
            player.GetBonusesComp.DeactivateMagnet();
            player.GetBonusesComp.DeactivateShild();
        }
        movableObject.HurtBox.enabled = false;
        movableObject.GetFollowingCamera.enabled = false;
        while (movableObject.transform.position.z > -100)
        {
            movableObject.transform.position = new Vector3(
                movableObject.transform.position.x, 
                movableObject.transform.position.y, 
                movableObject.transform.position.z - swapSpeed);
            yield return new WaitForSeconds(0.02f);
        }
        movableObject.gameObject.SetActive(false);
        if (isPlayer)
            player.playerMovement.ToMiddlePos();
        else
            movableObject.transform.position = new Vector3(0, 1.033f, -100);

    }

    private IEnumerator ObjectForward(PlayerBase movableObject, bool isPlayer = false)  
    {
        movableObject.transform.position = new Vector3(movableObject.transform.position.x, 0.34f, movableObject.transform.position.z);
        movableObject.gameObject.SetActive(true);
        while (movableObject.transform.position.z < -80)
        {
            movableObject.transform.position = new Vector3(movableObject.transform.position.x, movableObject.transform.position.y, movableObject.transform.position.z + swapSpeed);
            yield return new WaitForSeconds(0.02f);
        }
        movableObject.HurtBox.enabled = true;
        movableObject.GetFollowingCamera.enabled = true;
        movableObject.GetFollowingCamera.MainCamera = mainCamera.GetCamera;
        mainCamera.player = movableObject.GetFollowingCamera;

        if (isPlayer)
            player.Animator.Play("Run");
    }
    private IEnumerator CarCDTimer()
    {
        isCD = true;
        yield return new WaitForSeconds(vehicleCDDur);
        isCD = false;
    }
    void ClearEventsListeners()
    {
        OnRevival.RemoveAllListeners();
    }
}
