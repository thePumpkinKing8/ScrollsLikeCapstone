using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardAddStrategy", menuName = "SOs/CardStrategy/CardAddStrategy")]
public class CardAddStrategy : CardEffectStrategy
{
    [SerializeField] private CardData _cardData;
    public override void ApplyEffect(ICardEffectable target,int value, CardData card)
    {
        for(int i = 0; i < value; i++)
        {
            CardGameManager.Instance.AddCardToDeck(_cardData);
        }
        CardGameManager.Instance.EffectDone();
    }
}
