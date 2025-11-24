using UnityEngine;

public class GusanoController : EnemyController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetGameData.Playerlife <= 0)
        {
            attacking = false;
            animator.SetBool("Attacking", false);
            animator.SetBool("PlayerDetected", false);
            return;
        }
        Vector3 distance = player.position - transform.position;
        float distanceSqr = distance.sqrMagnitude;
        if (distanceSqr <= Mathf.Pow(stopDistance, 2))
        {
            attacking = true;
        }
        
        if (attacking == true)
        {
            animator.SetBool("Attacking", true);
            if (distanceSqr > Mathf.Pow(stopDistance, 2))
            {
                attacking = false;
                animator.SetBool("Attacking", false);
            }
        }
    }
}

