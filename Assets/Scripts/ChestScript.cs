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
    private ParticleSystem particulas, burbujas;
    [SerializeField] 
    private GameObject light;
    [SerializeField]
    private AudioClip cofre;
    [SerializeField]
    private AudioClip pocion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        burbujas.Play();
        animator = GetComponent<Animator>();
        switch (gemaName)
        {
            case "DobleSalto":
                if (GameManager.instance.GetGameData.MaxJumps>1)
                {
                    GetComponent<Collider2D>().enabled = false;
                }
                break;
            case "Vida":
                if (GameManager.instance.GetGameData.Playerlife >= GameManager.instance.GetGameData.PlayerMaxLife)
                {
                    GetComponent<Collider2D>().enabled = false;
                    light.SetActive(false);
                    animator.SetTrigger("CofreAbrir");
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
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger == true)
        {
            if(Input.GetButtonDown("Action"))
            {
                AudioManager.Instance.PlaySFX(cofre);
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
                AudioManager.Instance.PlaySFX(pocion);
                GameManager.instance.GetGameData.Playerlife = GameManager.instance.GetGameData.PlayerMaxLife;
                light.SetActive(false);
                particulas.Stop();

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
