using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum BossStates { Waiting, Jumping, Roar, Roll, Spikes, Tired, Death};

    [Header("Variables Generales")]
    [SerializeField]
    private BossStates currentState;
    private Transform player;
    private Animator animator;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        //Temporal
        currentState = BossStates.Jumping;
        ChangeState();
  
        
    }

    // Update is called once per frame
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

                break;
            case BossStates.Tired:

                break;
            case BossStates.Death:
                break;
        }

    }
    IEnumerator WaitingCoroutine()
    {
        yield return new WaitForSeconds(1);
        currentState = (BossStates)Random.Range(1, 5);
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

            if(t<=0.5f)
            {
                posY = Mathf.Lerp(puntoA.x, puntoBX, t); //Se frene
            }
            else
            {
                posY = Mathf.Lerp(puntoA.y, transform.position.y, 2-(t*2)); //Accelere
            }

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
    }
}
