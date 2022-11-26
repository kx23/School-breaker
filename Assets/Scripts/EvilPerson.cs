using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPerson : PlayerBase
{
    [SerializeField] private GameObject player;
    private float pos;
    public bool start = true;
    private float speedx,speedy,speedz;
    private Animator animator;
    private void OnEnable()
    {
        if (start)
        {
            animator.Play("Charge");
            StartPursuit();
            if (!player.GetComponent<Player>().isDead)
                StartCoroutine(pursuitTimer());
            start = false;
            return;
        }
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z - 20);
        StartPursuit();
        if (!player.GetComponent<Player>().isDead) 
            StartCoroutine(pursuitTimer());
        
    }
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnRevival.AddListener(() => animator.Play("Run"));
        player.GetComponent<Player>().OnCatchedBack.AddListener(() => animator.Play("Catch"));

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, speedx * Time.deltaTime), Mathf.Lerp(transform.position.y, player.transform.position.y, speedy * Time.deltaTime),Mathf.Lerp(transform.position.z, player.transform.position.z-pos, speedz * Time.deltaTime));
        GetGroundChecker.CheckGround();
        animator.SetBool("isGrounded", GetGroundChecker.IsGrounded);

    }

    public IEnumerator pursuitTimer()
    {
        yield return new WaitForSeconds(5f);
        if (player.GetComponent<Player>().isDead && player.GetComponent<Player>().BackCatched)
            yield break;
        pos = 20f;
        speedz = 0.3f;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public void StartPursuit()
    {
        
        pos = 2.5f;
        speedx = 20f;
        speedy = 15f;
        speedz = 4f;
    }
    private void SetHighHurtBox()
    { }
}
