using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private void Start()
    {
        BlakesAudioManager.Instance.PlayMusic("LoseBGM");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Anna_Gym");
    }
}
