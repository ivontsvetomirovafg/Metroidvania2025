using UnityEngine;

public class BossDor : MonoBehaviour
{
    private Animator animator;
    private LevelManagerBoss levelManagerBoss;
    [SerializeField]
    private AudioClip door;
    [SerializeField]
    private bool puertaDos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        levelManagerBoss = GameObject.Find("LevelManager").GetComponent<LevelManagerBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (puertaDos == true )
        {
            if (GameManager.instance.GetGameData.Boss1 == true)
            {
                animator.SetBool("Close", false);
                GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                GetComponent<Collider2D>().isTrigger = false;
            }   
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (puertaDos == false)
        {

            if(collision.gameObject.tag == "Player")
            {
             if (transform.position.x < collision.transform.position.x)
                {
                   AudioManager.Instance.PlaySFX(door);
                   animator.SetBool("Close", true);
                   GetComponent<Collider2D>().isTrigger = false;
                   levelManagerBoss.StartBattle();
                }
            }
        }
    }
}
