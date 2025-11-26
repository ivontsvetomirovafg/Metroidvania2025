using UnityEngine;

public class Pocion : MonoBehaviour
{
    [SerializeField]
    private GameObject iconUI;
private bool inTrigger;
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
            if (GameManager.instance.GetGameData.MaxJumps > 1)
            {
                GetComponent<Collider2D>().enabled = false;
            }
            break;
        case "TripleSalto":

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
    if (inTrigger == true)
    {
        if (Input.GetButtonDown("Action"))
        {
            animator.SetTrigger("CofreAbrir");
            iconUI.SetActive(false);
            Time.timeScale = 0;
        }
    }
}

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.tag == "Player")
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
        case "TripleSalto":

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
