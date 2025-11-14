using UnityEngine;

public class BossDor : MonoBehaviour
{
    private Animator animator;
    private LevelManagerBoss levelManagerBoss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        levelManagerBoss = GameObject.Find("LevelManager").GetComponent<LevelManagerBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (transform.position.x < collision.transform.position.x)
            {
                animator.SetBool("Close", true);
                GetComponent<Collider2D>().isTrigger = false;
                levelManagerBoss.StartBattle();
            }
        }
    }
}
