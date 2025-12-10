using UnityEngine;
using UnityEngine.UI;

public class Levelmanager : MonoBehaviour
{
    [SerializeField]
    private Image lifeBar;
    [SerializeField]
    private Image manaBar;
    [SerializeField]
    private Transform[] doorsPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        if (GameManager.instance.comeFromLoadGame == true)
        {
            GameManager.instance.comeFromLoadGame = false;
            GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("SaveStone").transform.position;
            GameObject.FindGameObjectWithTag("Player").transform.rotation = GameObject.Find("SaveStone").transform.rotation;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = doorsPoints[GameManager.instance.doorToGo].position;
            GameObject.FindGameObjectWithTag("Player").transform.rotation = doorsPoints[GameManager.instance.doorToGo].rotation;
        }
        UpdateLife();
        UpdateMana();
    }

    public void UpdateLife()
    {
        lifeBar.fillAmount = GameManager.instance.GetGameData.Playerlife / GameManager.instance.GetGameData.PlayerMaxLife;
    }

    public void UpdateMana()
    {
        manaBar.fillAmount = GameManager.instance.GetGameData.PlayerMana / GameManager.instance.GetGameData.PlayerMaxMana;
    }
}
