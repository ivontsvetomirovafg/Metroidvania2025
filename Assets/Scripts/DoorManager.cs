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
        GetComponent<Collider2D>().isTrigger = false;
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<CharacterControler>();
        animator.SetBool("Close", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player causa");
            iconUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Plausa");
            iconUI.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Lol");
            if(Input.GetButtonDown("Action"))
            {
               if(GameManager.instance.GetGameData.Key == player.key)
                {
                    animator.SetBool("Close", false);
                    GetComponent<Collider2D>().isTrigger = true;
                    enabled = false; 
                }
               else
                {
                  Debug.Log("No puedes");
                }
            }
        }              
    }
}
