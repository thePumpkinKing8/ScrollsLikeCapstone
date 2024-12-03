using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardBurnStrategy", menuName = "SOs/CardStrategy/CardBurnStrategy")]
public class CardBurnStrategy : CardEffectStrategy
{
    [SerializeField] private CardData _cardData;
    private void OnEnable()
    {
        _event = _cardEventData.GameEnd;
        _event.AddListener(RemoveCards);
    }
    public override void ApplyEffect(ICardEffectable target,int value ,CardData card)
    {
        GameManager.Instance.PlayersDeck.RemoveCardFromDeck(card);
    }

    public void RemoveCards()
    {
        var cardsToRemove = GameManager.Instance.PlayersDeck.Deck.FindAll(x => x ==  _cardData );
        foreach (var card in cardsToRemove)
        {
            GameManager.Instance.PlayersDeck.RemoveCardFromDeck(card);
        }
    }
}
