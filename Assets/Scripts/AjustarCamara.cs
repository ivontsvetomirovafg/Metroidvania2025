using UnityEngine;

public class AjustarCamara : MonoBehaviour
{
    [Header("New Cam")]
    [SerializeField]
    public float newMinX;
    public float newMaxX;
    public float newMinY;
    public float newMaxY;

    [Header("Old Cam")]
    [SerializeField]
    public float oldMinX;
    public float oldMaxX;
    public float oldMinY;
    public float oldMaxY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (transform.position.x > collision.transform.position.x)
            {
                CamaraController cam = Camera.main.GetComponent<CamaraController>();
                cam.minX = newMinX;
                cam.maxX = newMaxX;
                cam.minY = newMinY;
                cam.maxY = newMaxY;
            }
            else
            {
                CamaraController cam = Camera.main.GetComponent<CamaraController>();
                cam.minX = oldMinX;
                cam.maxX = oldMaxX;
                cam.minY = oldMinY;
                cam.maxY = oldMaxY;
            }
        }
    }
}
