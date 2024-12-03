using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StanceStrategy", menuName = "SOs/CardStrategy/StanceStrategy")]
public class CardStanceResolveStrategy : CardEffectStrategy
{
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        StanceZone.Instance.AddStance(card as StanceData);
    }
}
