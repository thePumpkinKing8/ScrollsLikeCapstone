using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInventoryUI : Singleton<DeckInventoryUI>
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject inventoryUI;

    private bool isInventoryOpen = false;

    private void Start()
    {
        inventoryUI.SetActive(false);
        PopulateDeckUI();
    }

    public void PopulateDeckUI()
    {
        ClearExistingCards();

        if (GameManager.Instance != null)
        {
            List<CardData> deck = GameManager.Instance.PlayersDeck.Deck;
            foreach (CardData card in deck)
            {
                GameObject cardObject = Instantiate(cardPrefab, gridParent);
                GameCard gameCard = cardObject.GetComponent<GameCard>();
                gameCard.ReferenceCardData = card;
            }
        }
        else
        {
            Debug.LogWarning("GameManager instance not found in the scene.");
        }
    }

    private void ClearExistingCards()
    {
        foreach (Transform child in gridParent)
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
        isInventoryOpen = true;
        inventoryUI.SetActive(true);
        Time.timeScale = 0; 
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    private void CloseInventory()
    {
        isInventoryOpen = false;
        inventoryUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }
}
