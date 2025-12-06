using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField] 
    private GameObject panelPause;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void RestartButton()
    {  
        AudioManager.Instance.SetMusicVolume(0.6f);
        GameManager.instance.GetGameData.Playerlife = GameManager.instance.GetGameData.PlayerMaxLife;
        GameManager.instance.GetGameData.PlayerMana = GameManager.instance.GetGameData.PlayerMaxMana;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuButton()
    {
        AudioManager.Instance.SetMusicVolume(0.6f);
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        AudioManager.Instance.FadeOutMusic(1.5f);
        panelGameOver.SetActive(true);
    }
    public void Pause()
    {
        if(panelPause.activeInHierarchy == false)
        {
            AudioManager.Instance.FadeOutMusic(1.5f);
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            AudioManager.Instance.SetMusicVolume(0.6f);
            panelPause.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
