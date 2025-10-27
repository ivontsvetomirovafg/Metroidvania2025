using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = speed * transform.right;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag!= "Player")
        {
            animator.SetTrigger("Dead");
            speed = 0;
        }
    }

    public void DestroyBall()
    {
        Destroy(gameObject);
    }
}
