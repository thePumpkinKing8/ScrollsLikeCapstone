using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeologyStrategy", menuName = "SOs/CardStrategy/GeologyStrategy")]
public class GeologyStrat : CardEffectStrategy
{
    [SerializeField] private StanceData _cardData;
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        TrackingManager.Instance.Geology = true;
        _cardEventData.StanceDone.AddListener(DeactivateEffect);
        CardGameManager.Instance.EffectDone();
    }

    public void DeactivateEffect(StanceData data)
    {
        if(data = _cardData)
        {
            TrackingManager.Instance.Geology = false;
            _cardEventData.StanceDone.RemoveListener(DeactivateEffect);
        }          
    }
}
