using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator animator;
    private CharacterControler player;
    [SerializeField]
    private GameObject iconUI;
    [SerializeField]   
    private bool inTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<CharacterControler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inTrigger = true;
            iconUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inTrigger = false;
            iconUI.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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
                }
               else
                {
                  Debug.Log("No puedes");
                }
            }
        }              
    }
}
