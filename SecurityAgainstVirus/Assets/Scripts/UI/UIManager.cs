using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsUI;

    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCredits()
    {
        creditsUI.SetActive(true);
    }

    public void EscapeCredits()
    {
        creditsUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
