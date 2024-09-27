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
    #endregion

    
    
}
