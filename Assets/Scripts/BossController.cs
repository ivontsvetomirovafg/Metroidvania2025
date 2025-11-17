using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum BossStates { Waiting, Jumping, Roar, Roll, Spikes, Death};

    [Header("Variables Generales")]
    [SerializeField]
    private BossStates currentState;
    private Transform player;
    private Animator animator;
    [SerializeField]
    private float bossLife;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float knockBackForce;
    [SerializeField]
    private Sprite muertoSprite;

    [Header ("Waiting")]
    [SerializeField]
    private float waitingTime;

    [Header("Jumping")]
    [SerializeField]
    private float maxJump = 12;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float timeToJump;

    [Header("Roar")]
    [SerializeField]
    private GameObject escapatrajoPrefab;
    [SerializeField]
    private Transform escapatrajoSpawnPoint;
    [SerializeField]
    private float timeToSpawn;

    [Header("Roll")]
    [SerializeField]
    private float timeToRoll;
    [SerializeField]
    private float colliderSizeX;
    [SerializeField]
    private float rollSpeed;
    private bool collisioned;
    public ContactPoint2D[] puntosContacto;

    [Header("Spikes")]
    [SerializeField]
    private GameObject spikesPrefab;
    [SerializeField]
    private Transform[] spikesSpawnPoints;
    [SerializeField]
    private float spikesTime;
    [SerializeField]
    private float tiredTime;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        currentState = BossStates.Spikes;
        ChangeState();
    }

    // Update is called once per frame
    public void SetDeathAtStart()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        GetComponent<SpriteRenderer>().sprite = muertoSprite;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        this.enabled = false;
    }

    void Update()
    {
        
    }

    void ChangeState()
    {
        switch (currentState)
        {
            case BossStates.Waiting:
                StartCoroutine(WaitingCoroutine());
                break;
            case BossStates.Jumping:
                StartCoroutine(JumpCoroutine());
                break;
            case BossStates.Roar:
                StartCoroutine(RoarCoroutine());
                break;
            case BossStates.Roll:
                StartCoroutine(RollCoroutine());
                break;
            case BossStates.Spikes:
                StartCoroutine(SpikesCoroutine());
                break;
            case BossStates.Death:
                break;
        }

    }
    IEnumerator WaitingCoroutine()
    {
        if(transform.position.x < player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        yield return new WaitForSeconds(1);

        if (transform.position.x < player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        yield return new WaitForSeconds(1);
        currentState = (BossStates)Random.Range(1, 5);
        ChangeState();
    }
    IEnumerator JumpCoroutine()
    {
        animator.SetBool("Jumping", true);
        yield return new WaitForSeconds(timeToJump);

        Vector2 puntoA = transform.position;
        float puntoBX = player.position.x;
        float t = 0;

        while (t<1)
        {
            t += Time.deltaTime * jumpSpeed;

            float posX = Mathf.Lerp(puntoA.x, puntoBX, t);
            float posY = puntoA.y + (4 * maxJump * t * (1 - t));

            transform.position = new Vector2(posX, posY);
            yield return null;
        }

        animator.SetBool("Jumping", false);
        currentState = BossStates.Waiting;
        ChangeState();
    }
    IEnumerator RoarCoroutine()
    {
        animator.SetBool("Roar", true);
        yield return new WaitForSeconds(timeToSpawn);
        Instantiate(escapatrajoPrefab, escapatrajoSpawnPoint.position, escapatrajoSpawnPoint.rotation);
        animator.SetBool("Roar", false);
        yield return new WaitForSeconds(timeToSpawn);
        
        currentState = BossStates.Waiting;
        ChangeState();
    }
    IEnumerator RollCoroutine()
    {
        animator.SetBool("Roll", true);
        collisioned = false;
        yield return new WaitForSeconds(timeToRoll);
        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        float standarColliderX = collider.size.x;
        collider.size = new Vector2(colliderSizeX, collider.size.y);
        
        while(collisioned == false)
        {
            transform.Translate(Vector3.left * rollSpeed * Time.deltaTime, Space.Self);
            yield return null;
        }
        animator.SetBool("Roll", false);
        collider.size = new Vector2(standarColliderX, collider.size.y);
        yield return new WaitForSeconds(timeToRoll);
        currentState = BossStates.Waiting;
        ChangeState();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        puntosContacto = collision.contacts;

        if(collision.GetContact(puntosContacto.Length-1).normal.y > -0.5f && collision.GetContact(puntosContacto.Length-1).normal.y < 0.5f)
        {
            if (collision.GetContact(puntosContacto.Length-1).normal.x > 0.5f || collision.GetContact(puntosContacto.Length-1).normal.x < -0.5f)
            {
                collisioned = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag== "Player")
        {
            collision.gameObject.GetComponent<CharacterControler>().TakeDamage(damage);
            ContactPoint2D point = collision.GetContact(0);
            if(transform.position.x <player.position.x) //derecha
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockBackForce);
            }
            else //izquierda
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(point.normal * knockBackForce);
            }
            StartCoroutine(collision.gameObject.GetComponent<CharacterControler>().KnockBackCoroutine());
        } 


        /*if (point.normal.y < 0)
        {
            if(collision.GetContact(0).normal.x > 0)//derecha
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right*knockBackForce);
                StartCoroutine(collision.gameObject.GetComponent<CharacterControler>().KnockBackCoroutine());
            }
            else //izquierda
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockBackForce);
            }
        }
        else
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(point.normal * knockBackForce);
            StartCoroutine(collision.gameObject.GetComponent<CharacterControler>().KnockBackCoroutine());
        }
    }    */
    }
    IEnumerator SpikesCoroutine()
    {
        animator.SetBool("Spikes", true);
        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        float standarColliderX = collider.size.x;
        collider.size = new Vector2(colliderSizeX, collider.size.y);
       
        yield return new WaitForSeconds(spikesTime);
        //Por si acaso queremos hacer algo en medio
        yield return new WaitForSeconds(tiredTime);
        animator.SetBool("Spikes", false);
        collider.size=new Vector2(standarColliderX,collider.size.y);
        currentState= BossStates.Waiting;
        ChangeState();
    }

    public void ShootSpikes()
    {
        for (int i = 0; i <spikesSpawnPoints.Length; i ++)
        {
            Instantiate(spikesPrefab, spikesSpawnPoints[i].position, spikesSpawnPoints[i].rotation);
        }
    }

    public void TakeDamage(float _damage)
    {
        bossLife -= _damage;
        if(bossLife <= 0)
        {
            //muerto
            animator.SetTrigger("Death");
            StopAllCoroutines();
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            this.enabled = false;
            GameManager.instance.GetGameData.Boss1 = true;
        }
        else
        {

        }
    }
}
