using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject DeathUI;
    private bool isDead = false;

    // Update is called once per frame

    private void Start()
    {
        if (InputManager.Instance.IsDead)
            InputManager.Instance.EnableGameplay();

        DeathUI.SetActive(false);
        isDead = false;
    }

    public void onDead()
    {
        DeathUI.SetActive(true);
        isDead = true;
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
