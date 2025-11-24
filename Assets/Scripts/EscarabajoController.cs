using UnityEngine;

public class EscarabajoController : EnemyController
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
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("Attacking", false);
            animator.SetBool("PlayerDetected", false);
            return;
        }
        base.Update();
        if (attacking == true)
        {
            animator.SetBool("Attacking", true);

            Vector3 distance = player.position - transform.position;
            float distanceSq = distance.sqrMagnitude;
            if (distanceSq > Mathf.Pow(stopDistance, 2))
            {
                attacking = false;
                animator.SetBool("Attacking", false);
            }
        }
    }
}
