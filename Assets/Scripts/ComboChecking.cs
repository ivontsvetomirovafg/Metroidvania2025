using UnityEngine;

public class ComboChecking : MonoBehaviour
{
    private CharacterControler player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<CharacterControler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FinishAttack1()
    {
        player.CheckCombo1();
    }
    public void FinishAttack2()
    {
        player.CheckCombo2();
    }
    public void FinishAttack3()
    {
        player.FinishHeavyAttack();
    }
}
