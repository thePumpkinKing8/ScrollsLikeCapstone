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

    public void AddCardToPositon(int position = 0)
    {

    }

    public void ShuffleCardsIn(List<CardData> cardsToShuffle)
    {

    }

    public void DrawCard()
    {

    }
}
