using JetBrains.Annotations;
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        levelManager = GameObject.Find("LevelManager").GetComponent<Levelmanager>();
    }

    // Update is called once per frame
    void Update()

    {
        if (comboCount == 0) 
        { //Movimiento
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
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount++;
            }

            CheckJump();

            if(Input.GetButtonDown("FireBall"))
            {
                if (coldDown <= timePassFireBall && GameManager.instance.GetGameData.PlayerMana >= fireBallCost)
                {
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
                comboCount = Mathf.Clamp(comboCount + 1, 0, 2);
                animator.SetInteger("Combo", comboCount);

            }
            if(Input.GetButtonDown("Fire2") && comboCount==0)
            {
                animator.SetTrigger("AtackHeavy");
                comboCount = 1; 
            }
        }

        timePassFireBall += Time.deltaTime;
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
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(GameManager.instance.GetGameData.PlayerDamage);
            }
            else
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(GameManager.instance.GetGameData.HeavyDamage);
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
            animator.SetTrigger("Death");
            //Sacar panel GameOver
            //Cuando vuele
        }
        else
        {
            //Recibe daño
            animator.SetTrigger("Hit");
        }
    }
}
