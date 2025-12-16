using UnityEngine;
using UnityEngine.SceneManagement;
public class WIN : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void MainMenu()
    {
        AudioManager.Instance.SetMusicVolume(0.6f);
        SceneManager.LoadScene(0);
    }
}
