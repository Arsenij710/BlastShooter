using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);

        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
