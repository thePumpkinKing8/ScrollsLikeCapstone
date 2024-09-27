using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "DeckData", menuName = "SOs/DeckData")]
public class PlayerDeck : ScriptableObject
{
    [SerializeField] private List<CardData> _startingCards; //the cards the player will start with at the beginning of the game
    public List<CardData> Deck { get { return _deck; } }
    private List<CardData> _deck;


    public void Initialize()
    {
        foreach (var card in _startingCards)
        {
            _deck.Add(card);
        }
    }
    public void AddCardToDeck(CardData card)
    {
        _deck.Add(card);
    }

    public void RemoveCardFromDeck(CardData card)
    {
        _deck.Remove(card);
    }

    public void ClearDeck()
    {
        _deck.Clear();
    }

}
