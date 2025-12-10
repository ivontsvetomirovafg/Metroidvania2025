using UnityEngine;
using UnityEngine.SceneManagement;
public class CamaraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 camOffset;
    [SerializeField]
    public float minX, maxX, minY, maxY;
    
    void Start()
    {
        UpdateCam();
    }

    public void UpdateCam()
    {
        minX = GameManager.instance.MinX;
        maxX = GameManager.instance.MaxX;
        minY = GameManager.instance.MinY;
        maxY = GameManager.instance.MaxY;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(player.position.x,minX,maxX);
        float y = Mathf.Clamp(player.position.y, minY, maxY);

        transform.position = new Vector3(x, y+camOffset.y, camOffset.z);
    }
}
