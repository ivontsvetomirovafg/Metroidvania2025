using UnityEngine;

public class LevelManagerBoss : Levelmanager
{
    [SerializeField]
    private BossController boss;
    [SerializeField]
    private Animator[] doorAnimator;

    private void Start()
    {
        base.Start();
        if (GameManager.instance.GetGameData.Boss1 == true)
        {
            for (int i = 0; i < doorAnimator.Length; i++)
            {
                doorAnimator[i].GetComponent<Collider2D>().enabled = false;
            }
            boss.SetDeathAtStart();
        }
    }
    public void StartBattle()
    {
        boss.enabled = true;
        /*for (int i = 0; i < doorAnimator.Length; i++)
        {
            doorAnimator[i].SetBool("Close", true);

        }*/
        foreach(Animator anim in doorAnimator)
        {
            anim.SetBool("Close", true);
        }

        //Empiece la musica de boss
        //Barra de vida boss
        //UFX
    }
}
