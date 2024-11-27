using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInventoryUI : Singleton<DeckInventoryUI>
{
   // [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _gridParent;
    [SerializeField] private GameObject _inventoryUI;

    private bool isInventoryOpen = false;

    private void Start()
    {
        _inventoryUI.SetActive(false);
        PopulateDeckUI();
    }

    private void OnEnable()
    {
        PopulateDeckUI();
    }

    public void PopulateDeckUI()
    {
        ClearExistingCards();

        if (GameManager.Instance != null)
        {
            List<CardData> deck = GameManager.Instance.PlayersDeck.Deck;
            Debug.Log(deck.Count);
            foreach (CardData card in deck)
            {
                GameCard gameCard = PoolManager.Instance.Spawn("Card").GetComponent<GameCard>();
                gameCard.ReferenceCardData = card;
                gameCard.transform.SetParent(_gridParent);
            }
        }
        else
        {
            Debug.LogWarning("GameManager instance not found in the scene.");
        }
    }

    private void ClearExistingCards()
    {
        foreach (Transform child in _gridParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void ToggleInventory()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    private void OpenInventory()
    {
        GameManager.Instance.SetPause();
        isInventoryOpen = true;
        _inventoryUI.SetActive(true);
        Time.timeScale = 0; 
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    private void CloseInventory()
    {
        isInventoryOpen = false;
        _inventoryUI.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.ResumeGame();
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }
}
