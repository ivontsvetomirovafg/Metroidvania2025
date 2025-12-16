using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    private Animator animator;
    private CharacterControler player;
    [SerializeField]
    private GameObject iconUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<CharacterControler>();
        animator.SetBool("Close", true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            iconUI.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            iconUI.SetActive(false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Input.GetButtonDown("Action"))
            {
               if(player.key == true)
                {
                    animator.SetBool("Close", false);
                    GetComponent<Collider2D>().isTrigger = true;
                    enabled = false; 
                    iconUI.SetActive(false);
                }
               else
                {
                  Debug.Log("You need a key");
                }
            }
        }              
    }
}
