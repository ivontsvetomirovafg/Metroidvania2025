using System.Collections;
using UnityEngine;

public class HadaController : EnemyController
{
    [SerializeField]
    private float downDistance;
    [SerializeField]
    private float speedDown;
    private bool animacionBajar;
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
            StartCoroutine(BajarAtaque());
            Vector3 distance = player.position - transform.position;
            float distanceSq = distance.sqrMagnitude;
            if (distanceSq > Mathf.Pow(stopDistance, 2))
            {
                attacking = false;
                animator.SetBool("Attacking", false);
            }
        }      
    }
    IEnumerator BajarAtaque ()
    {
        if (animacionBajar == false)
        {

            animacionBajar = true;
            Vector3 posIni = transform.position;
            Vector3 posFin = transform.position - new Vector3(0, downDistance, 0);
            float t = 0;
            while (t < 1)
         {
                t += Time.deltaTime * speedDown;
                transform.position = Vector3.Lerp(posIni, posFin, t);
             yield return null;
         }
          while (t > 0)
         {
             t -= Time.deltaTime * speedDown;
             transform.position = Vector3.Lerp(posIni, posFin, t);
             yield return null;

          }
          animacionBajar = false;    
        }
    }
}
