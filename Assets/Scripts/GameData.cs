using UnityEngine;
using System;

[Serializable]
public class GameData 
{
    [SerializeField]
    private float playerLife;
    [SerializeField]
    private float playerMaxLife;
    [SerializeField]
    private float playerMana;
    [SerializeField]
    private float playerMaxMana;
    [SerializeField]
    private float playerDamage;
    [SerializeField]
    private float fireballDamage;
    [SerializeField]
    private float heavyDamage;
    [SerializeField]
    private int sceneSave;
    [SerializeField]
    private int maxJumps;
    [SerializeField]
    private bool boss1;
 
    public float Playerlife
    {
        get { return playerLife; }
        set { playerLife = value; }
    }

    public float PlayerMana
    {
        get { return playerMana; }
        set { playerMana = value; }
    }
    public float PlayerMaxMana
    {
        get { return playerMaxMana; }
        set { playerMaxMana = value; }
    }
    public float PlayerMaxLife
    {
        get { return playerMaxLife; }
        set { playerMaxLife = value; }
    }
    public float PlayerDamage
    {
        get { return playerDamage; }
        set { playerDamage = value; }
    }
    public float FireballDamage
    {
        get { return fireballDamage; }
        set { fireballDamage = value; }
    }
    public float HeavyDamage
    {
        get { return heavyDamage; }
        set { heavyDamage = value; }
    }
    public int SceneSave
    {
        get { return sceneSave; } 
        set { sceneSave = value; }
    }
    public int MaxJumps
    {
        get { return maxJumps; }
        set { maxJumps = value; }
    }
    public bool Boss1
    {
        get { return boss1; } 
        set { boss1 = value; }
    }
}
