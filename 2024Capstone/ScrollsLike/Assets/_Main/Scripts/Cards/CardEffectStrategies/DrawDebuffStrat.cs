using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DrawDebuffStrat", menuName = "SOs/CardStrategy/DrawDebuffStrat")]
public class DrawDebuffStrat : CardEffectStrategy
{
    private void OnEnable()
    {
        _event = _cardEventData.DrawPhaseStartEvent;
    }

    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        HandController.Instance.DrawMod -= value;
    }
}
