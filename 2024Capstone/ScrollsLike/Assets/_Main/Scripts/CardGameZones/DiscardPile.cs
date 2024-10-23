using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    public List<CardData> DiscardedCards { get { return _discardedCards; } }
    private List<CardData> _discardedCards;

    private void Awake()
    {
        CardGameManager.Instance.Events.DrawFailed.AddListener(ShuffleCardsToDeck);
    }
    private void Start()
    {
        CardGameManager.Instance.Events.DrawFailed.AddListener(ShuffleCardsToDeck);
    }
    public void AddCard(CardData discardedCard)
    {
        _discardedCards.Add(discardedCard);
    }

    public void ShuffleCardsToDeck()
    {
        _discardedCards.Clear();
    }

    public void RemoveACard(CardData chosenCard)
    {
        _discardedCards.Remove(chosenCard);
    }
}
