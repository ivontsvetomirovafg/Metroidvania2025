using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public DoorManager door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        door.OnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        door.OnTriggerExit2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        door.OnTriggerStay2D(collision);
    }
}
