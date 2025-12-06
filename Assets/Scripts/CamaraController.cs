using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 camOffset;
    [SerializeField]
    public float minX, maxX, minY, maxY;
    private Levelmanager levelManager;

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(player.position.x,minX,maxX);
        float y = Mathf.Clamp(player.position.y, minY, maxY);

        transform.position = new Vector3(x, y+camOffset.y, camOffset.z);
    }
    
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<Levelmanager>();
    }
    
    public void UpdateCam()
    {
        minX = levelManager.MinX;
        maxX = levelManager.MaxX;
        minY = levelManager.MinY;
        maxY = levelManager.MaxY;      
    }
}
