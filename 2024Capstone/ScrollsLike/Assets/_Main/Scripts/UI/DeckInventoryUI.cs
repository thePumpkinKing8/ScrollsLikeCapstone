using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInventoryUI : Singleton<DeckInventoryUI>
{
   // [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _gridParent;
    [SerializeField] private GameObject _inventoryUI;

    private bool _isInventoryOpen = false;
    private bool _wasDungeon;

    private void Start()
    {
        _inventoryUI.SetActive(false);
        PopulateDeckUI();
    }

    private void OnEnable()
    {
        PopulateDeckUI();
    }

    public void PopulateDeckUI(List<CardData> cardsToDisplay = null)
    {
        ClearExistingCards();

        

        if (GameManager.Instance != null)
        {
            List<CardData> deck = new List<CardData>();
            if (cardsToDisplay != null)
                deck = cardsToDisplay;
            else
                deck = GameManager.Instance.PlayersDeck.Deck;
            Debug.Log(deck.Count);
            foreach (CardData card in deck)
            {
                GameCard gameCard = PoolManager.Instance.Spawn("Card").GetComponent<GameCard>();
                gameCard.ReferenceCardData = card;
                gameCard.transform.SetParent(_gridParent);
                gameCard.GetComponent<Canvas>().overrideSorting = false;
            }
        }
        else
        {
            Debug.LogWarning("GameManager instance not found in the scene.");
        }
    }

    private void ClearExistingCards()
    {
        foreach (PoolObject child in _gridParent.GetComponentsInChildren<PoolObject>())
        {
            child.OnDeSpawn();
        }
    }

    public void ToggleInventory(List<CardData> cardsToDisplay = null)
    {
        if (_isInventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory(cardsToDisplay);
        }
    }

    private void OpenInventory(List<CardData> cardsToDisplay = null)
    {
        if(GameManager.Instance.State == GameState.Dungeon)
        {
            _wasDungeon = true;
            GameManager.Instance.SetPause();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        _isInventoryOpen = true;
        _inventoryUI.SetActive(true);
        PopulateDeckUI(cardsToDisplay);
        Time.timeScale = 0; 
        
    }

    private void CloseInventory()
    {
        _isInventoryOpen = false;
        _inventoryUI.SetActive(false);
        Time.timeScale = 1;
        if(_wasDungeon)
        {
            GameManager.Instance.ResumeGame();
            _wasDungeon = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }      
        
    }
}
