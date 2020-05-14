using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToOptions()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
