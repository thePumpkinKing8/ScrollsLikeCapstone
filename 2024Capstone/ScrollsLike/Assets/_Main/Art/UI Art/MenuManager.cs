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
        SceneManager.LoadScene(1);
    }

    void OnClick()
    {

    }


    public void Quit()
    {

#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }

#else
        Application.Quit();
#endif


    }
}