using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CardEventData", menuName = "SOs/CardEventData")]
public class CardEventData : ScriptableObject
{
    //Events that are called to 
    #region EventSetup
    //card draw and discard events
    public UnityEvent<CardData> CardDrawnEvent;
    public UnityEvent DrawCardEvent;
    public UnityEvent<CardData> CardDiscardedEvent;
    public UnityEvent DrawFailed;
    public UnityEvent<List<CardData>> ShuffleCardsToDeck;
    public UnityEvent<GameCard> AddCardToHand;

    //phase events
    public UnityEvent DrawPhaseStartEvent;
    public UnityEvent DrawPhaseEndEvent;
    public UnityEvent PrepPhaseStartEvent;
    public UnityEvent PrepPhaseEndEvent;
    public UnityEvent PlayPhaseStartEvent;
    public UnityEvent PlayPhaseEndEvent;
    public UnityEvent ResolutionPhaseStartEvent;
    public UnityEvent ResolutionPhaseEndEvent;
    public UnityEvent CleanupPhaseStartEvent;
    public UnityEvent CleanupPhaseEndEvent;

    //input events
    public UnityEvent<GameCard> PlayCard;

    //effect events
    public UnityEvent<List<CardEffect>> EffectPlayed;
    public UnityEvent EffectEnded;

    public UnityEvent<int> EnergyGain;

    public UnityEvent<int> PlayerHit;
    public UnityEvent<int> EnemyHit;
    public UnityEvent<CardData> StanceResolved;
    public UnityEvent<int> PlayerGainsBlock;
    #endregion



}
