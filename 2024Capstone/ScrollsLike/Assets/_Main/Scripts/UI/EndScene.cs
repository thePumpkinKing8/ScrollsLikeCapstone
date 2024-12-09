using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    private void Start()
    {
        BlakesAudioManager.Instance.PlayMusic("EndBGM");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
