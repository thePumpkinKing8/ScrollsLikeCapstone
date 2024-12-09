using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShriekStrategy", menuName = "SOs/CardStrategy/ShriekStrategy")]
public class GhastlyShriekStrat : CardEffectStrategy
{
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        HealthManager.Instance.ApplyEffect(CardEffectType.Damage, value + CardGameManager.Instance.CardsInDiscard, card);
    }
}
