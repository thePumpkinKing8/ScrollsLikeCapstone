using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiscardPile : MonoBehaviour
{
    public List<CardData> DiscardedCards { get { return _discardedCards; } }
    private List<CardData> _discardedCards = new List<CardData>();

    [SerializeField] private Image _pileObj;
    [SerializeField] private Sprite _emptyImg;
    [SerializeField] private Sprite _notEmptyImg;

    private void Awake()
    {
        
    }
    private void Start()
    {
       
    }
    public void AddCard(CardData discardedCard)
    {
        _discardedCards.Add(discardedCard);
        _pileObj.sprite = _notEmptyImg;
    }

    public void ShuffleCardsToDeck()
    {
        _discardedCards.Clear();
        _pileObj.sprite = _emptyImg;
    }

    public void RemoveACard(CardData chosenCard)
    {
        _discardedCards.Remove(chosenCard);
    }

    public void Display()
    {
        DeckInventoryUI.Instance.ToggleInventory(DiscardedCards);
    }
}
