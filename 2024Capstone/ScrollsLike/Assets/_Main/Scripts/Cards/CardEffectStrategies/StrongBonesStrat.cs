using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StrongBonesStrategy", menuName = "SOs/CardStrategy/StrongBonesStrategy")]
public class StrongBonesStrat : CardEffectStrategy
{
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        target.ApplyEffect(CardEffectType.Block, CardGameManager.Instance.CardsInDiscard, card);
    }
}
