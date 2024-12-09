using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        pauseMenuUI.SetActive(false);
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame()
    {
        if(InputManager.Instance.IsPaused)
            InputManager.Instance.EnableGameplay();
        
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {      
        pauseMenuUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Anna_Gym");
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        Destroy(GameManager.Instance);
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenOptionsMenu()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
