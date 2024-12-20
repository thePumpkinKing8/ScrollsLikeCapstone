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
    public UnityEvent GameStartEvent;
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


    public UnityEvent GameEnd;
    //input events
    public UnityEvent<GameCard> PlayCard;
    public UnityEvent MoveToNextSlot;

    //effect events
    public UnityEvent EffectManagerPermission;

    public UnityEvent<List<CardEffect>> EffectPlayed;
    public UnityEvent EffectEnded;

    public UnityEvent<int> EnergyChange;

    public UnityEvent PlayerHit;
    public UnityEvent EnemyHit;
    public UnityEvent EnemyBlocked;
    public UnityEvent EnemyHeal;
    public UnityEvent StanceResolved;
    public UnityEvent PlayerGainsBlock;
    public UnityEvent AttackPlayed;
    #endregion



}
