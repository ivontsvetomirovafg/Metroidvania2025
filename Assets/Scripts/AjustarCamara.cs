using UnityEngine;

public class AjustarCamara : MonoBehaviour
{
    [SerializeField]
    public float newMinX, newMixX, newMinY, newMaxY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
        CamaraController cam = Camera.main.GetComponent<CamaraController>();
    
        }
    }
}
