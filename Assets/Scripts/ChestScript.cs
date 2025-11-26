using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField]
    private GameObject iconUI;
    [SerializeField]   
    private bool inTrigger;
    private bool inLife;
    private Animator animator;
    [SerializeField]
    private string gemaName;
    private Animator animator2;
    [SerializeField]
    private ParticleSystem particulas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (gemaName)
        {
            case "DobleSalto":
                if (GameManager.instance.GetGameData.MaxJumps>1)
                {
                    GetComponent<Collider2D>().enabled = false;
                }
                break;
            case "Vida":
                if (GameManager.instance.GetGameData.Playerlife > 100)
                {
                    GetComponent<Collider2D>().enabled = false;
                }
                break;
            case "Dash":

                break;
            case "ExtraDamage":
                if (GameManager.instance.GetGameData.PlayerDamage > 25)
                {
                    GetComponent<Collider2D>().enabled = false;
                }
                break;
            default:

                break;
        }
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger == true)
        {
            if(Input.GetButtonDown("Action"))
            {
                animator.SetTrigger("CofreAbrir");
                iconUI.SetActive(false);
                Time.timeScale = 0;
            }
        }
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
    public void ObtenerGema()
    {
        switch (gemaName)
        {
            case "DobleSalto":
                GameManager.instance.GetGameData.MaxJumps = 2;
                break;
            case "Vida":

                GameManager.instance.GetGameData.Playerlife = GameManager.instance.GetGameData.PlayerMaxLife;
                break;
            case "Dash":

                break;
            case "ExtraDamage":
                GameManager.instance.GetGameData.PlayerDamage = 35;
                break;

            default:

                break;
        }

        Time.timeScale = 1;
        GetComponent<Collider2D>().enabled = false;
    }
    public void ActivateParticles()
    {
        particulas.Play();
    }
}
