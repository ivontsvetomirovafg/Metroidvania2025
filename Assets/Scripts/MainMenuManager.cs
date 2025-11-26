using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelSlots;
    private bool newGame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton(bool _newGame)
    {
        panelSlots.SetActive(true);
        newGame = _newGame;
    }
    public void SlotButton(int _slot)
    {
        if (newGame == true)
        {
            GameManager.instance.GetGameData = new GameData();
            GameManager.instance.slot = _slot;
            GameManager.instance.GetGameData.Playerlife = 100;
            GameManager.instance.GetGameData.PlayerMana = 50;
            GameManager.instance.GetGameData.PlayerMaxLife = 100;
            GameManager.instance.GetGameData.PlayerMaxMana = 50;
            GameManager.instance.GetGameData.PlayerDamage = 25;
            GameManager.instance.GetGameData.FireballDamage = 15;
            GameManager.instance.GetGameData.HeavyDamage = 50;
            GameManager.instance.GetGameData.MaxJumps = 1;

            SceneManager.LoadScene(1);
        }
        else
        {
            if (PlayerPrefs.HasKey("data" + _slot.ToString()))
            {
                GameManager.instance.slot = _slot;
                GameManager.instance.LoadGame();
                GameManager.instance.comeFromLoadGame = true;
              SceneManager.LoadScene(GameManager.instance.GetGameData.SceneSave);
            }
            else
            {
                GameManager.instance.GetGameData = new GameData();
                GameManager.instance.slot = _slot;
                GameManager.instance.GetGameData.Playerlife = 100;
                GameManager.instance.GetGameData.PlayerMana = 50;
                GameManager.instance.GetGameData.PlayerMaxLife = 100;
                GameManager.instance.GetGameData.PlayerMaxMana = 50;
                GameManager.instance.GetGameData.PlayerDamage = 25;
                GameManager.instance.GetGameData.FireballDamage = 15;
                GameManager.instance.GetGameData.HeavyDamage = 50;
                GameManager.instance.GetGameData.MaxJumps = 1;

                SceneManager.LoadScene(1);
            }
        }
     
    }
}
