using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplyStatusStrategy", menuName = "SOs/CardStrategy/ApplyStatusStrategy")]
public class ApplyStatusStrategy : CardEffectStrategy
{
    [SerializeField] private StanceTrigger _statusEffect;
    public override void ApplyEffect(ICardEffectable target, CardData card)
    {
        target.AddEffect(_statusEffect);
    }
}
