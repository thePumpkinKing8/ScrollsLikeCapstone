using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardGameEventManager : Singleton<CardGameEventManager>
{
    #region EventSetup
    public UnityEvent<CardData> CardDrawnEvent;
    #endregion

    public void HandleCardDraw(CardData card) => CardDrawnEvent.Invoke(card);
}
