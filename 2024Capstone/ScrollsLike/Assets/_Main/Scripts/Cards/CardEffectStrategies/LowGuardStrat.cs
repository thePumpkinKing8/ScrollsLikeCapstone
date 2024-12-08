using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LGStrategy", menuName = "SOs/CardStrategy/LGStrategy")]
public class LowGuardStrat : CardEffectStrategy
{
    [SerializeField] private StanceData _cardData;
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        EnemyManager.Instance.DamageMod += 2;
        _cardEventData.StanceDone.AddListener(DeactivateEffect);
    }

    public void DeactivateEffect(StanceData data)
    {
        if (data = _cardData)
        {
            EnemyManager.Instance.DamageMod += 2;
            _cardEventData.StanceDone.RemoveListener(DeactivateEffect);
        }
    }
}
