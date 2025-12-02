using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float life;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float speed;
    private bool playerDetected;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform player;
    public float stopDistance;
    public bool attacking;
    public bool estoyMuerto;
    
    [SerializeField]
    private AudioClip dead;
    [SerializeField]
    private AudioClip hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    public void Update()
    {
        if (estoyMuerto == true)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        if (playerDetected == true && attacking == false)
        {
            Vector3 distancia = player.transform.position - transform.position;
            if(distancia.x>0) //derecha
            {
                rb.linearVelocity = new Vector2 (1 * speed, rb.linearVelocity.y);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (distancia.x<0) //izquierda
            {
                rb.linearVelocity= new Vector2 (-1 * speed, rb.linearVelocity.y);
                transform.eulerAngles = Vector3.zero;
            }

            Vector3 distance=player.position - transform.position;
            float distanceSqr= distance.sqrMagnitude;
            if (distanceSqr <= Mathf.Pow(stopDistance,2))
            {
                attacking = true;
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().linearVelocity= Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag=="Player"))
        {            
            animator.SetTrigger("Alert");
            Invoke("StartMoving", animator.GetCurrentAnimatorStateInfo(0).length);
            player = collision.transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag==("Player"))
        {
            collision.gameObject.GetComponent<CharacterControler>().TakeDamage(damage);
        }
    }
    private void StartMoving()
    {
        playerDetected = true;
        animator.SetBool("PlayerDetected", true);
    }
    public void TakeDamage(float _damage)
    {
        life-=_damage;
        if (life <=0)
        {
            //muerte
            AudioManager.Instance.PlaySFX(dead);

            animator.SetTrigger("Death");
            rb.gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            estoyMuerto = true;
        }
        else
        {
            //hit
            AudioManager.Instance.PlaySFX(hit);
            animator.SetTrigger("Hit");
        }
    }
}
