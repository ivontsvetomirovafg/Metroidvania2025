using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform cam;
    [SerializeField]
    private float porcentaje;
    private Vector3 previousPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        previousPos = cam.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 loQueSeMaMovido = cam.position - previousPos;
        transform.Translate(loQueSeMaMovido * porcentaje);
        previousPos = cam.position;
    }
}
