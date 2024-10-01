using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CardGameManager : Singleton<CardGameManager>
{
    public Phase CurrentPhase { get; private set; }
    public CardEventData Events { get { return _cardEventData; } }
    [SerializeField] private CardEventData _cardEventData;

    #region EventFunctions
    public void HandleCardDraw(CardData card) => Events.CardDrawnEvent.Invoke(card);
    public void HandleCardDiscard(CardData card) => Events.CardDiscardedEvent.Invoke(card);
    public void DrawFromDeckFailed() => Events.DrawFailed.Invoke();
    public void HandleShuffleToDeck(List<CardData> cards) => Events.ShuffleCardsToDeck.Invoke(cards);
    public void DrawCard() => Events.DrawCardEvent.Invoke();
    public void AddCardToHand(GameCard card) => Events.AddCardToHand.Invoke(card);
    public void DrawPhaseStart()
    {
        CurrentPhase = Phase.DrawPhase;
        Events.DrawPhaseStartEvent.Invoke();
    }
   
    public void DrawPhaseEnd() => Events.DrawPhaseEndEvent.Invoke();
    public void PrepPhaseStart()
    {
        CurrentPhase = Phase.PrepPhase;
        Events.PrepPhaseStartEvent.Invoke();
    }
    
    public void PrepPhaseEnd() => Events.PrepPhaseEndEvent.Invoke();
    public void PlayPhaseStart()
    {
        CurrentPhase = Phase.PlayPhase;
        Events.PlayPhaseStartEvent.Invoke();
    }
    public void PlayPhaseEnd() => Events.PlayPhaseEndEvent.Invoke();
    public void ResolutionPhaseStart()
    {
        CurrentPhase = Phase.ResolutionPhase;
        Events.ResolutionPhaseStartEvent.Invoke();        
    }
    public void ResolutionPhaseEnd() => Events.ResolutionPhaseEndEvent.Invoke();
    public void CleanupPhaseStart()
    {
        CurrentPhase = Phase.CleanupPhase;
        Events.CleanupPhaseStartEvent.Invoke();       
    }
    public void CleanupPhaseEnd() => Events.CleanupPhaseEndEvent.Invoke();
    public void PlayCard(GameCard  card) => Events.PlayCard.Invoke(card);
    public void EffectActivate(List<CardEffect> effects) => Events.EffectPlayed.Invoke(effects);
    public void EffectDone() => Events.EffectEnded.Invoke();
    #endregion

    private void Start()
    {
        Invoke("LateStart", Time.deltaTime);
    }

    private void LateStart()
    {
        DrawPhaseStart();
    }
}

public enum Phase
{
    DrawPhase,
    PrepPhase,
    PlayPhase,
    ResolutionPhase,
    CleanupPhase
}
