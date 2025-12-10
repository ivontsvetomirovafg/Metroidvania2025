using UnityEngine;
using UnityEngine.SceneManagement;
public class Puerta : MonoBehaviour
{
    [SerializeField]
    private int sceneToGo;
    [SerializeField]
    private int doorPoint;
    private Levelmanager levelManager;

    [Header("Cam")]
    [SerializeField]
    public float MinX = 0;
    public float MaxX = 23;
    public float MinY = 0;
    public float MaxY = 5;

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<Levelmanager>();
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (sceneToGo == 3)
            {
             GameManager.instance.MinX = MinX;
             GameManager.instance.MaxX = MaxX;
             GameManager.instance.MinY = MinY;
             GameManager.instance.MaxY = MaxY;
            }
            levelManager.UpdateLife();
            levelManager.UpdateMana();
            
            GameManager.instance.doorToGo = doorPoint;
            SceneManager.LoadScene(sceneToGo);   
        }
    }
}
