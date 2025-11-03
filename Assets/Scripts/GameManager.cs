using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private GameData gameData;
    public int slot;
    public int doorToGo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameData GetGameData
    {
        get { return gameData; }
        set { gameData = value; }
    }

    public void SaveGame()
    {
        string data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("data"+slot.ToString(), data);
    }
    public void LoadGame()
    {
        if(PlayerPrefs.HasKey("data1") == true)
        {
            string data = PlayerPrefs.GetString("data"+slot.ToString());
            gameData= JsonUtility.FromJson<GameData>(data);
        }
    }
}
