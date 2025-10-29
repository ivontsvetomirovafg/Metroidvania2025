using UnityEngine;

public class GameData 
{
    private float playerLife;
    private float playerMaxLife;
    private float playerMana;
    private float playerMaxMana;

    public float Playerlife
    {
        get { return playerLife; }
        set { playerLife = value; }
    }
}
