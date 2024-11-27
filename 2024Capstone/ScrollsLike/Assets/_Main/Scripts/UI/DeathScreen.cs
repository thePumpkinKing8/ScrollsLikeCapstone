using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject DeathUI;

    // Update is called once per frame
    private void Start()
    {
        DeathUI.SetActive(false);
    }
    public void onDead()
    {
        DeathUI.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        SceneManager.LoadScene("Anna_Gym");
    }
}
