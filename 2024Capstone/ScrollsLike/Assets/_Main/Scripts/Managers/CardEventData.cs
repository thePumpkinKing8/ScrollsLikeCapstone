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
    public UnityEvent<CardData> CardDiscardEvent;
    public UnityEvent DrawFailed;
    public UnityEvent<List<CardData>> ShuffleCardsToDeck;
    #endregion

    public void HandleCardDraw(CardData card) => CardDrawnEvent.Invoke(card);
    public void HandleCardDiscard(CardData card) => CardDiscardEvent.Invoke(card);
    public void DrawFromDeckFailed() => DrawFailed.Invoke();
    public void HandleShuffleToDeck(List<CardData> cards) => ShuffleCardsToDeck.Invoke(cards);
    public void DrawCard() => DrawCardEvent.Invoke();
}
