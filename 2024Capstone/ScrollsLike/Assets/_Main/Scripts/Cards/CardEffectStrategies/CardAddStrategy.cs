using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardAddStrategy", menuName = "SOs/CardStrategy/CardAddStrategy")]
public class CardAddStrategy : CardEffectStrategy
{
    [SerializeField] private CardData _cardData;
    [SerializeField] private int _numberOfCards = 1;
    public override void ApplyEffect(ICardEffectable target,int value, CardData card)
    {
        for(int i = 0; i<= _numberOfCards; i++)
        {
            GameManager.Instance.PlayersDeck.AddCardToDeck(_cardData);
        }
       
    }
}
