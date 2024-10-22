using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private PlayerDeck _deckData;
    public List<CardData> Deck {get{ return _deck; }}
    private List<CardData> _deck = new List<CardData>();
    private void Awake()
    {
        CardGameManager.Instance.Events.DrawCardEvent.AddListener(DrawCard);
        CardGameManager.Instance.Events.ShuffleCardsToDeck.AddListener(ShuffleCardsIn);
    }

    void Start()
    {
        _deckData = GameManager.Instance.PlayersDeck;
        ShuffleCardsIn(_deckData.Deck);   
    }
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
    public void DrawCard()
    {
        if(_deck.Count < 1)
        {
            CardGameManager.Instance.DrawFromDeckFailed();
        }
        var drawnCard = _deck[0];
        _deck.Remove(drawnCard);
        CardGameManager.Instance.HandleCardDraw(drawnCard);
    }

    private void OnApplicationQuit() //clears added cards from the decks data. this will only be done by starting a new run or by losing in the full game
    {
        _deckData.ClearDeck();
    }
}
