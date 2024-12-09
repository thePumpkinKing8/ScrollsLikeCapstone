using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _slides = new List<GameObject> ();
    [SerializeField] private MainMenu _menu;
    private int slideIndex;
    public void StartTutorial()
    {
        _slides[0].SetActive(true);
        slideIndex = 0;
    }

    public void NextSlide()
    {
        if(slideIndex >= _slides.Count - 1)
        {
            _slides[slideIndex].SetActive(false);
            _menu.EnableMainMenu();
            gameObject.SetActive(false);
            return;
        }
        BlakesAudioManager.Instance.PlayAudio("Button");
        _slides[slideIndex].SetActive(false);
        slideIndex++;
        _slides[slideIndex].SetActive(true);
    }

    public void EndTutorial()
    {
        _slides[slideIndex].SetActive(false);
        _menu.EnableMainMenu();
        gameObject.SetActive(false);
    }
}
