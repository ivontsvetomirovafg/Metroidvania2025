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
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    public void Update()
    {
        if (playerDetected == true)
        {
            Vector3 distancia = player.transform.position - transform.position;
            if(distancia.x>0) //derecha
            {
                rb.linearVelocity = speed * Vector2.right;
            }
            else if (distancia.x<0) //izquierda
            {
                rb.linearVelocity=speed * Vector2.left;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag=="Player"))
        {
            playerDetected = true;
            player = collision.transform;
        }
    }
}
