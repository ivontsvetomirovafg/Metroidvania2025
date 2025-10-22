using UnityEngine;

public class CharacterControler : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private int jumpCount;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float groundDistance;

    //temp
    private int maxJumps = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(speed * horizontal, rb.linearVelocity.y);

        if(horizontal == 0)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }
        if(horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(horizontal > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        if(Input.GetButtonDown("Jump") && jumpCount<maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }

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
    
}
