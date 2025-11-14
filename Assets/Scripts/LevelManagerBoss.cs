using UnityEngine;

public class LevelManagerBoss : Levelmanager
{
    [SerializeField]
    private BossController boss;
    [SerializeField]
    private Animator[] doorAnimator;

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
