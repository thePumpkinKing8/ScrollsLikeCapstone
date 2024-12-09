using UnityEngine;
using UnityEngine.UI;

public class RestUI : MonoBehaviour
{
    [SerializeField] private Button restButton;
    [SerializeField] private GameObject restUI;
    [SerializeField] private DiscardMenu discardMenu;

    public void OnDiscardButtonPressed()
    {
        BlakesAudioManager.Instance.PlayAudio("Button");
        discardMenu.gameObject.SetActive(true);  // Open the discard menu when the player presses the discard button
        discardMenu.PopulateDeckUI();
    }

    void Start()
    {
        restUI.SetActive(false);
    }

    public void OnRestButtonClicked()
    {
        BlakesAudioManager.Instance.PlayAudio("Button");
        GameManager.Instance.FullyRegenerateHealth();
        GameManager.Instance.HideRestUI();
    }

}
