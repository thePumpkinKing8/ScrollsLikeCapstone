using UnityEngine;
using UnityEngine.UI;

public class RestUI : MonoBehaviour
{
    [SerializeField] private Button restButton;
    [SerializeField] private GameObject restUI;
    [SerializeField] private DiscardMenu discardMenu;

    public void OnDiscardButtonPressed()
    {
        discardMenu.OpenDiscardMenu();  // Open the discard menu when the player presses the discard button
    }

    void Start()
    {
        restUI.SetActive(false);
    }

    public void OnRestButtonClicked()
    {
        GameManager.Instance.FullyRegenerateHealth();
        GameManager.Instance.HideRestUI();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        DungeonLevelLoader levelLoader = FindObjectOfType<DungeonLevelLoader>();
        if (levelLoader != null)
        {
            levelLoader.LoadNextLevel();
        }
    }

}
