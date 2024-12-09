using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject splashScreen;
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        BlakesAudioManager.Instance.PlayAudio("MenuBGM");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            splashScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("Anna_Gym");
    }

    void OnClick()
    {

    }


    public void Quit()
    {

        Application.Quit();

    }
}