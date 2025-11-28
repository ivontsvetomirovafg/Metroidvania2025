using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform tpPoint;
    [SerializeField]
    private AudioClip musicSong;
    [SerializeField]
    private AudioClip gameOverSound;
    [SerializeField]
    private AudioClip nextLevelSound;

    //UI
    [SerializeField]
    private Text coinsText;
    [SerializeField]
    private Text heartsText;
    [SerializeField]
    private Text starsText;
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField] 
    private GameObject panelPause;
    [SerializeField]
    private GameObject panelLevelCompleted;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCoins();
        UpdateLives();
        AudioManager.instance.PlayMusic(musicSong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartButton()
    {
        
        GameData.instance.life = 3;
        GameManager.instance.totalCoins = 0;
        GameManager.instance.totalStars = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevelButton()
    {
        GameManager.instance.totalCoins = 0;
        GameManager.instance.life = 3;
        GameManager.instance.totalStars = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlaySFXSound(gameOverSound);
        panelGameOver.SetActive(true);
        Time.timeScale = 0;
    }
    public void Pause()
    {
        if(panelPause.activeInHierarchy == false)
        {
            panelPause.SetActive(true);
            AudioManager.instance.StopMusic();
            Time.timeScale = 0;
        }
        else
        {
            panelPause.SetActive(false);
            AudioManager.instance.PlayMusic(musicSong);
            Time.timeScale = 1;
        }
    }
    public void FinishLevel()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlaySFXSound(nextLevelSound);
        panelLevelCompleted.SetActive(true);
    }

}
