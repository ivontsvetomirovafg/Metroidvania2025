using UnityEngine;

public class LanzaPeter : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<CharacterControler>().TakeDamage(damage);
            Debug.Log("Pupa al player");
        }
        Destroy(gameObject);
    }
}
