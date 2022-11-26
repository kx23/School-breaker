using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//TODO Есть доля секунды где персонаж между сменой платформы не может прыгать 
public class Player : PlayerBase
{
    
    private bool isCatched = false;
    public bool movable = false;
    public bool IsCatched { get => isCatched; set => isCatched = value; }
    private bool backCatched = false;
    public bool BackCatched { get => backCatched; set => backCatched = value; }


    
    public UnityEvent OnDead = new UnityEvent(),
                      OnStunned = new UnityEvent(),
                      OnCatchedFront = new UnityEvent(),
                      OnCatchedBack = new UnityEvent();
    
    public bool isDead = false;
    
    private new Rigidbody rigidbody;
    public Rigidbody Rigidbody { get => rigidbody; }
    
    private Animator animator;
    public Animator Animator { get => animator; }
    private bool isStunned = false;
    public bool IsStunned { get => isStunned; }
    [SerializeField] private GameObject HeadHurtBox;
    private AudioSource audioSource;
    public AudioSource AudioSource { get => audioSource; }
    //[SerializeField] private BoxCollider HurtBox;
    [SerializeField] private BoxCollider bubbleBox;
    private BonusesComp bonusesComp;
    public BonusesComp GetBonusesComp { get => bonusesComp; }
    public PlayerMovement playerMovement;
   


    protected override void Awake()
    {
        base.Awake();
        bonusesComp = GetComponent<BonusesComp>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerMovement = new PlayerMovement(this);
    }
    
    private void Start()
    {

        Physics.gravity = new Vector3(0, -15.0F, 0);
        OnDead.AddListener(() => {

                isDead = true;
                animator.Play("Dead");
                bonusesComp.DeactivateMagnet();
                bonusesComp.DeactivateShild();
                rigidbody.velocity = Vector3.zero;
                PlatformMovement.isMove = false;
                GetGroundChecker.FindCurrentPlatform();
                SwipeManager.swipeEnded = true;
            }
        );

        OnCatchedBack.AddListener(() => { isCatched = true; backCatched = true; });
        OnCatchedFront.AddListener(() => { isCatched = true; backCatched = false; });
        OnStunned.AddListener(playerMovement.PastPos);
        OnStunned.AddListener(() => StartCoroutine(StunTimer()));
    }

    public void Immobolize(bool value)
    {
        rigidbody.useGravity = !value;
        if (value)
            rigidbody.velocity = new Vector3(0f, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || !movable) return;

        GetGroundChecker.CheckGround();
        animator.SetBool("isGrounded", GetGroundChecker.IsGrounded);

        playerMovement.Move();


        if (IsDoubleTap())
        {
                if (GetGroundChecker.IsGrounded && PlayerPrefs.GetInt("WhoopeeCushionsCount")>0)
                {
                rigidbody.AddForce(new Vector3(0, 50f, 0), ForceMode.Impulse);
                PlayerPrefs.SetInt("WhoopeeCushionsCount", PlayerPrefs.GetInt("WhoopeeCushionsCount")-1);
                    return;
                }
        }

#if UNITY_EDITOR
        {
            if (Input.GetMouseButtonDown(1)&& GetGroundChecker.IsGrounded && (PlayerPrefs.GetInt("WhoopeeCushionsCount") > 0))
            {

                rigidbody.AddForce(new Vector3(0, 50f, 0), ForceMode.Impulse);
                PlayerPrefs.SetInt("WhoopeeCushionsCount", PlayerPrefs.GetInt("WhoopeeCushionsCount") - 1);
                return;
            }
        }
#endif

        playerMovement.DetectMovements();
        
    }

    private void OnDisable()
    {
        bonusesComp.DeactivateMagnet();
        bonusesComp.DeactivateShild();
        //StopAllCoroutines();
    }
    private void SetSmallHurtBox()
    {
        HurtBox.center = new Vector3(0, -0.591266632f, -0.103158705f);
        HurtBox.size = new Vector3(1.7f, 0.236062109f, 0.84146148f);
        //HeadHurtBox.SetActive(false);
        bubbleBox.size = new Vector3(0.8f, 0.183403254f, 0.270000011f);
        bubbleBox.center = new Vector3(0, -0.37829861f, -0.0299999975f);
    }

    private void SetHighHurtBox()
    {
        HurtBox.center = new Vector3(0, -0.235155776f, -0.103158705f);
        HurtBox.size = new Vector3(1.7f, 1.2f, 0.84146148f);
        //HeadHurtBox.SetActive(true);
        bubbleBox.size = new Vector3(0.8f, 0.72f, 0.27f);
        bubbleBox.center = new Vector3(0, -0.11f, -0.03f);
    }
    private IEnumerator StunTimer()
    {
        isStunned = true;
        yield return new WaitForSeconds(.5f);
        isStunned = false;
    }
    public static bool IsDoubleTap()
    {
        bool result = false;
        float MaxTimeWait = 1;
        float VariancePosition = 1;

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float DeltaTime = Input.GetTouch(0).deltaTime;
            float DeltaPositionLenght = Input.GetTouch(0).deltaPosition.magnitude;

            if (DeltaTime > 0 && DeltaTime < MaxTimeWait && DeltaPositionLenght < VariancePosition)
                result = true;
        }
        return result;
    }

}
