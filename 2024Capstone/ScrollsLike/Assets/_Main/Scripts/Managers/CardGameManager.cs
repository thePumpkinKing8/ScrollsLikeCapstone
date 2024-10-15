using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : Singleton<CardGameManager>
{
    public Phase CurrentPhase { get; private set; }
    public CardEventData Events { get { return _cardEventData; } }
    [SerializeField] private CardEventData _cardEventData;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
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
    public void PlayerHit(int damage) => Events.PlayerHit.Invoke(damage);
    public void EnemyHit(int damage) => Events.EnemyHit.Invoke(damage);
    public void StanceResolved(CardData data) => Events.StanceResolved.Invoke(data);
    #endregion

    private void Start()
    {
        GameManager.Instance.CardGameStart();
        Invoke("LateStart", 1);
    }

    //wait for all scripts to get their start and awake functions finished before starting the game
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
