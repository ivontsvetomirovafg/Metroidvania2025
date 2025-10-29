using UnityEngine;

public class GoblinController : EnemyController
{
    [SerializeField]
    private GameObject lanzaPrefab;
    [SerializeField]
    private Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (attacking == true)
        {
            animator.SetBool("Attacking", true);
            Vector3 distance = player.position-transform.position; 
            float distancesqr = distance.sqrMagnitude;
            if(distancesqr > Mathf.Pow(stopDistance, 2))
            {
                attacking = false;
                animator.SetBool("Attacking", false);
            }
        }
    }

    public void ShotLanza()
    {
        Instantiate(lanzaPrefab, spawnPoint.position, spawnPoint.rotation);

    }
}
