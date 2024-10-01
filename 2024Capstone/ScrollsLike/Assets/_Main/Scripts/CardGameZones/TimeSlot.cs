using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlot : MonoBehaviour
{
    public GameCard PlayersCard 
    { 
        get { return _playersCard; } 
        private set 
        {
            if(_playersCard != null)
            {
                RemoveCard();
            }
            _playersCard = value;
        } 
    }
    private GameCard _playersCard;
    public CardData EnemyCard { get; private set; } //placeholder for what the enemy will actually use



    public void AddCard(GameCard card)
    {
        PlayersCard = card;
        card.transform.SetParent(transform, true);
        card.transform.position = this.transform.position;
        card.InHand = false;
        card.InTimeSlot = true;
        card.transform.localScale = Vector3.zero;
    }

    public void RemoveCard()
    {
        CardGameManager.Instance.AddCardToHand(PlayersCard);
        _playersCard.OnDeSpawn();
        _playersCard = null;
       
    }

    public void AddEnemyEffect(CardData card)
    {
        EnemyCard = card;
       
    }

    public void ResolvePlayerEffects()
    {
        EffectManager.Instance.ActivateEffect(PlayersCard.ReferenceCardData.CardResolutionEffects);
    }

    public void ResolveEnemyEffects()
    {
        foreach (CardEffect effect in EnemyCard.CardResolutionEffects)
        {
            //activate effect.
        }
    }

    public void CleanUpPhase()
    {
        CardGameManager.Instance.HandleCardDiscard(PlayersCard.ReferenceCardData);
        PlayersCard.OnDeSpawn();
        CardGameManager.Instance.DrawPhaseStart();
    }
}
