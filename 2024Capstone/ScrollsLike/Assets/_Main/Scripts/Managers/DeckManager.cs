using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public List<CardData> Deck {get{ return _deck; }}
    private List<CardData> _deck;

   
    public void Shuffle()
    {
        _deck = _deck.OrderBy(x => Random.value).ToList();
    }

    public void AddCardToPositon(CardData card, int position = 0)
    {
        _deck.Insert(position, card);
    }

    public void ShuffleCardsIn(List<CardData> cardsToShuffle)
    {
        foreach(CardData card in cardsToShuffle)
        {
            _deck.Add(card);
        }
        Shuffle();
    }

    /// <summary>
    /// returns the top card of the deck and removes it from the deck
    /// </summary>
    /// <returns></returns>
    public CardData DrawCard()
    {
        var drawnCard = _deck[0];
        _deck.Remove(drawnCard);
        return drawnCard;
    }
}
