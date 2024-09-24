using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "CardData", menuName = "Card")]
public class CardData : ScriptableObject
{

    public string CardName { get { return _cardName; } }
    public CardType CardType { get { return _cardType; } }
    public Texture CardImage { get { return _cardImage; } }
    public string CardDescription { get { return _cardDescription; } }

    [SerializeField] private string _cardName;
    [SerializeField] private CardType _cardType;
    [SerializeField] private Texture _cardImage;
    [SerializeField] private string _cardDescription;
    public List<CardEffect> CardResolutionEffects { get { return _cardResolutionEffects; } }
    [SerializeField] private List<CardEffect> _cardResolutionEffects;
    public List<CardEffect> CardOnDrawEffects { get { return _cardOnDrawEffects; } }
    [SerializeField] private List<CardEffect> _cardOnDrawEffects;
    public List<CardEffect> CardOnDiscardEffects { get { return _cardOnDiscardEffects; } }
    [SerializeField] private List<CardEffect> _cardOnDiscardEffects;
}



public enum CardType
{ 
    Strike = 0,
    Guard = 1,
    Maneuver = 2,
    Stance = 3
}

