using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiscardPile : MonoBehaviour
{
    public List<CardData> DiscardedCards { get { return _discardedCards; } }
    private List<CardData> _discardedCards = new List<CardData>();

    [SerializeField] private GameObject _pileObj;


    private void Awake()
    {
        
    }
    private void Start()
    {
       if(_discardedCards.Count < 1)
            _pileObj.SetActive(false);
    }
    public void AddCard(CardData discardedCard)
    {
        _discardedCards.Add(discardedCard);
        _pileObj.SetActive(true);
    }

    public void ShuffleCardsToDeck()
    {
        _discardedCards.Clear();
        _pileObj.SetActive(false);
        Debug.Log("clear");
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
