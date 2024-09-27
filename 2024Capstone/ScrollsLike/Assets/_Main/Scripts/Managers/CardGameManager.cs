using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CardGameManager : Singleton<CardGameManager>
{
    public CardEventData Events { get { return _cardEventData; } }
    [SerializeField] private CardEventData _cardEventData;

    #region EventFunctions
    public void HandleCardDraw(CardData card) => Events.CardDrawnEvent.Invoke(card);
    public void HandleCardDiscard(CardData card) => Events.CardDiscardedEvent.Invoke(card);
    public void DrawFromDeckFailed() => Events.DrawFailed.Invoke();
    public void HandleShuffleToDeck(List<CardData> cards) => Events.ShuffleCardsToDeck.Invoke(cards);
    public void DrawCard() => Events.DrawCardEvent.Invoke();
    public void DrawPhaseStart() => Events.DrawPhaseStartEvent.Invoke();
    public void DrawPhaseEnd() => Events.DrawPhaseEndEvent.Invoke();
    public void PrepPhaseStart() => Events.PrepPhaseStartEvent.Invoke();
    public void PrepPhaseEnd() => Events.PrepPhaseEndEvent.Invoke();
    public void PlayPhaseStart() => Events.PlayPhaseStartEvent.Invoke();
    public void PlayPhaseEnd() => Events.PlayPhaseEndEvent.Invoke();
    public void ResolutionPhaseStart() => Events.ResolutionPhaseStartEvent.Invoke();
    public void ResolutionPhaseEnd() => Events.ResolutionPhaseEndEvent.Invoke();
    public void CleanupPhaseStart() => Events.CleanupPhaseStartEvent.Invoke();
    public void CleanupPhaseEnd() => Events.CleanupPhaseEndEvent.Invoke();
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
