using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveStone : MonoBehaviour
{
    [SerializeField]
    private GameObject saveIcon;
    private bool inTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            saveIcon.SetActive(true);
            inTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            saveIcon.SetActive(false);
            inTrigger = false;
        }
    }
    private void Update()
    {
        if(inTrigger==true)
        {
            if(Input.GetAxis("Vertical")>0.5f)
            {
                //Guardar partida
                GameManager.instance.GetGameData.SceneSave = SceneManager.GetActiveScene().buildIndex;
                GameManager.instance.SaveGame();
                saveIcon.SetActive(false);
                inTrigger = false;
                //Efecto particulas
            }
        }
    }

}
