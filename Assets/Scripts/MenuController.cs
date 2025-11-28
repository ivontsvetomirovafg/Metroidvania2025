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
        GameManager.instance.GetGameData.Playerlife = GameManager.instance.GetGameData.PlayerMaxLife;
        GameManager.instance.GetGameData.PlayerMana = GameManager.instance.GetGameData.PlayerMaxMana;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        panelGameOver.SetActive(true);
    }
    public void Pause()
    {
        if(panelPause.activeInHierarchy == false)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            panelPause.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
