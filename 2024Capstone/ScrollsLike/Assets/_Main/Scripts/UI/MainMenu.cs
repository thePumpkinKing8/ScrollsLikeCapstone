using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Pages")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject tutorial;
    [SerializeField]
    private GameObject options;

    [Header("Main Menu Buttons")]
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button tutorialButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private List<Button> returnButtons;

    void Start()
    {
        BlakesAudioManager.Instance.PlayMusic("MenuBGM");
        EnableMainMenu();

        startButton.onClick.AddListener(StartGame);
        //optionsButton.onClick.AddListener(EnableOptions);
        tutorialButton.onClick.AddListener(Tutorial);
        exitButton.onClick.AddListener(OnExitGame);

        foreach (var item in returnButtons)
        {
            item?.onClick?.AddListener(EnableMainMenu);
        }
    }

    public void StartGame()
    {
        BlakesAudioManager.Instance.PlayAudio("Button");
        SceneManager.LoadScene("Anna_Gym");
    }

    public void EnableMainMenu()
    {
        BlakesAudioManager.Instance.PlayAudio("Button");
        mainMenu.SetActive(true);
        tutorial.SetActive(false);
        options.SetActive(false);
    }

    public void EnableOptions()
    {
        BlakesAudioManager.Instance.PlayAudio("Button");
        mainMenu.SetActive(true);
        tutorial.SetActive(false);
        options.SetActive(true);
    }
    public void Tutorial()
    {
        BlakesAudioManager.Instance.PlayAudio("Button");
        tutorial.GetComponent<TutorialController>().StartTutorial();
        mainMenu.SetActive(false);
        tutorial.SetActive(true);
        options.SetActive(false);
    }

    public void OnExitGame()
    {
        BlakesAudioManager.Instance.PlayAudio("Button");
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
