using JetBrains.Annotations;
using System.Collections;
using System.IO;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private int jumpCount;
    private int comboCount;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float groundDistance;
    private MenuController menu;
    [SerializeField]
    private float dashCooldown;
    [SerializeField]
    private float timeToDash;
    [SerializeField]
    private float dashSpeed;

    [SerializeField]
    private GameObject fireBallPrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private float fireBallCost;
    [SerializeField]
    private float coldDown;
    private float timePassFireBall;
    private Levelmanager levelManager;
    public bool knockBack;
    [SerializeField]
    private float knockbackTime;

    [SerializeField]
    private AudioClip jump;
    [SerializeField]
    private AudioClip fire;
    [SerializeField]
    private AudioClip selfHit;
    [SerializeField]
    private AudioClip ataqueBasico;
    [SerializeField]
    private AudioClip ataqueFuerte;
    [SerializeField]
    private AudioClip muerte;

    public bool key = false;
    [SerializeField]
    private float dashCol;
    [SerializeField]
    private float offsetCol;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        levelManager = GameObject.Find("LevelManager").GetComponent<Levelmanager>();
        menu = GameObject.Find("MenuController").GetComponent<MenuController>();
        key = GameManager.instance.GetGameData.Key;
    }

    // Update is called once per frame
    void Update()

    {
        if (knockBack == true)
        {
            return;
        }
        if (GameManager.instance.GetGameData.Playerlife <= 0)
        {
            return;
        }
        //Movimiento
        if (comboCount == 0) 
        { 
            float horizontal = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(speed * horizontal, rb.linearVelocity.y);

            if (horizontal == 0)
            {
                animator.SetBool("Run", false);
            }
            else
            {
                animator.SetBool("Run", true);
            }
            if (horizontal < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (horizontal > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            //Salto
            if (Input.GetButtonDown("Jump") && jumpCount < GameManager.instance.GetGameData.MaxJumps)
            {
                AudioManager.Instance.PlaySFX(jump);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount++;
            }

            CheckJump();

            if(Input.GetButtonDown("FireBall"))
            {
                if (coldDown <= timePassFireBall && GameManager.instance.GetGameData.PlayerMana >= fireBallCost)
                {
                    AudioManager.Instance.PlaySFX(fire);
                    Instantiate(fireBallPrefab, spawnPoint.position, spawnPoint.rotation);
                    GameManager.instance.GetGameData.PlayerMana -= fireBallCost;
                    levelManager.UpdateMana();
                    timePassFireBall = 0; 
                }
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Ataque
        if (jumpCount == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                AudioManager.Instance.PlaySFX(ataqueBasico);
                comboCount = Mathf.Clamp(comboCount + 1, 0, 2);
                animator.SetInteger("Combo", comboCount);

            }
            if(Input.GetButtonDown("Fire2") && comboCount==0)
            {
                AudioManager.Instance.PlaySFX(ataqueFuerte);
                animator.SetTrigger("AtackHeavy");
                comboCount = 1; 
            }
        }
        timePassFireBall += Time.deltaTime;
        
        //Dash 

         if (GameManager.instance.GetGameData.Dash == true)
            {
                if (Input.GetButtonDown("Dash"))
                {
                    StartCoroutine(DashEnum());
                }
            }
            timeToDash += Time.deltaTime;
         }

   public IEnumerator DashEnum()
   {
    CapsuleCollider2D colliderChange = GetComponent<CapsuleCollider2D>();

    float colliderNormal = colliderChange.size.y;
    float collider = colliderChange.offset.y;

    if (jumpCount == 0 && dashCooldown <= timeToDash)
    {
        animator.SetTrigger("Dash");


        colliderChange.offset = new Vector2(colliderChange.offset.x, offsetCol);
        colliderChange.size = new Vector2(colliderChange.size.x, dashCol);

        rb.AddForce(Vector3.right * dashSpeed);
        knockBack = true;
        timeToDash = 0;
    }
    else
    {
        rb.AddForce(Vector3.left * dashSpeed);
        knockBack = true;
    }


    yield return new WaitForSeconds(0.5f);

    knockBack = false;
    colliderChange.size = new Vector2(colliderChange.size.x, colliderNormal);
    colliderChange.offset = new Vector2(colliderChange.offset.x, offsetCol);
    }

    public void CheckCombo1()
    {
        if (comboCount < 2)
        {
            comboCount = 0;
            animator.SetInteger("Combo", comboCount);
        }
    }
    public void CheckCombo2()
    {
        comboCount = 0;
        animator.SetInteger("Combo", comboCount);
       
    }
    public void FinishHeavyAttack()
    {
        comboCount = 0; 

    }

    void CheckJump()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, groundDistance);
        bool isGrounded = false;
        for (int i = 0; i<coliders.Length; i++)
        {
            if (coliders[i].transform.tag=="Ground")
            {
                isGrounded = true;
            }
        }
        if(isGrounded == true)
        {
            jumpCount = 0;
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", true);
            if (jumpCount == 0)
            {
                jumpCount++;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            int comboAnimator = animator.GetInteger("Combo");
            if (comboAnimator > 0)
            {
                try
                {               
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(GameManager.instance.GetGameData.PlayerDamage);
                }
                catch
                {
                    collision.gameObject.GetComponent<BossController>().TakeDamage(GameManager.instance.GetGameData.PlayerDamage);
                }
            }
            else
            {
                try
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(GameManager.instance.GetGameData.HeavyDamage);
                }
                catch
                {
                    collision.gameObject.GetComponent<BossController>().TakeDamage(GameManager.instance.GetGameData.HeavyDamage);
                }                         
            }
        }
    }
    public void TakeDamage(float _damage)
    {
        GameManager.instance.GetGameData.Playerlife -= _damage;
        levelManager.UpdateLife();
        if(GameManager.instance.GetGameData.Playerlife <=0)
        {
            //Muerte
            AudioManager.Instance.PlaySFX(muerte);
            animator.SetTrigger("Death");
            rb.linearVelocity = Vector2.zero;
            this.enabled = false;       
            menu.GameOver();          
        }
        else
        {
            //Recibe da�o
            AudioManager.Instance.PlaySFX(selfHit);
            animator.SetTrigger("Hit");
        }
    }

    public IEnumerator KnockBackCoroutine()
    {
        knockBack = true;
        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        knockBack = false;
    }
}
