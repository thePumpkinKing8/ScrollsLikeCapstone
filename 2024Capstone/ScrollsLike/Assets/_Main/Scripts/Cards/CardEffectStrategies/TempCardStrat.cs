using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TempCardStrategy", menuName = "SOs/CardStrategy/TempCardStrategy")]
public class TempCardStrat : CardEffectStrategy
{
    [SerializeField] private CardData _cardData;
    public override void ApplyEffect(ICardEffectable target, int value, CardData card)
    {
        for(int i = 0; i < value; i++)
        {
            CardGameManager.Instance.GetCardFromDeck(_cardData);
        }
        CardGameManager.Instance.EffectDone();
    }

 
}
