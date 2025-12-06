using UnityEngine;
using UnityEngine.SceneManagement;

public class Puerta : MonoBehaviour
{
    [SerializeField]
    private int sceneToGo;
    [SerializeField]
    private int doorPoint;

    [Header("Camera Limits For This Door")] //prueba
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.doorToGo = doorPoint;
            SceneManager.LoadScene(sceneToGo);
        }
    }
}
