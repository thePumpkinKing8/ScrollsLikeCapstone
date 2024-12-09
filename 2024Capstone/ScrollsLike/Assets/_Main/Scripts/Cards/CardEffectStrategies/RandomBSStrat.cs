using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RandomStrategy", menuName = "SOs/CardStrategy/RandomStrategy")]
public class RandomBSStrat : CardEffectStrategy
{
    private int _damage;
    private void OnEnable()
    {
        _event = _cardEventData.CardDiscarded;
        _cardEventData.CardDiscardedEvent.AddListener(SetDamage);
    }
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        target.ApplyEffect(CardEffectType.Damage, _damage, card);
        Debug.Log(_damage);
    }

    public void SetDamage(CardData data)
    {
        _damage = data.EnergyCost;
    }
}
