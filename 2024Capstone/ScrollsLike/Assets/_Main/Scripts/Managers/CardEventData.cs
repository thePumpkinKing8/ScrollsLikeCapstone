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

    public void HandleCardDraw(CardData card) => CardDrawnEvent.Invoke(card);
    public void HandleCardDiscard(CardData card) => CardDiscardedEvent.Invoke(card);
    public void DrawFromDeckFailed() => DrawFailed.Invoke();
    public void HandleShuffleToDeck(List<CardData> cards) => ShuffleCardsToDeck.Invoke(cards);
    public void DrawCard() => DrawCardEvent.Invoke();
    public void DrawPhaseStart() => DrawPhaseStartEvent.Invoke();
    public void DrawPhaseEnd() => DrawPhaseEndEvent.Invoke();
    public void PrepPhaseStart() => PrepPhaseStartEvent.Invoke();
    public void PrepPhaseEnd() => PrepPhaseEndEvent.Invoke();
    public void PlayPhaseStart() => PlayPhaseStartEvent.Invoke();
    public void PlayPhaseEnd() => PlayPhaseEndEvent.Invoke();
    public void ResolutionPhaseStart() => ResolutionPhaseStartEvent.Invoke();
    public void ResolutionPhaseEnd() => ResolutionPhaseEndEvent.Invoke();
    public void CleanupPhaseStart() => CleanupPhaseStartEvent.Invoke();
    public void CleanupPhaseEnd() => CleanupPhaseEndEvent.Invoke();
    
}
