using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlot : MonoBehaviour
{
    public GameCard PlayersCard 
    { 
        get { return _playersCard; } 
        private set //if the player has already placed a card in this slot and attempts to place a new one. the old one will be returned to the players hand
        {
            if(_playersCard != null)
            {
               // RemoveCard();
            }
            _playersCard = value;
        } 
    }

    public List<GameCard> PlayerCards
    {
        get { return _playersCards; }
        private set { _playersCards = value; }
    }
    private List<GameCard> _playersCards = new List<GameCard>();
    private GameCard _playersCard;
    public EnemyCardData EnemyCard { get; private set; } 

    private bool _active = false;
    
    public void ToggleActive()
    {
        _active = !_active;
    }

    //adds a card to the timeslot 
    public void AddCard(GameCard card)
    {
        PlayerCards.Add(card);
        card.transform.SetParent(transform, true);
        card.transform.position = this.transform.position;
        card.InHand = false;
        card.InTimeSlot = true;
        card.transform.localScale = Vector3.zero;
    }

    public void RemoveCard(GameCard card)
    {
        CardGameManager.Instance.AddCardToHand(PlayersCard);
        GameCard removedCard = PlayerCards.Find( x => x == card);
        removedCard.OnDeSpawn();
        PlayerCards.Remove(removedCard);
       
    }

    public void DiscardCard()
    {
        CardGameManager.Instance.HandleCardDiscard(PlayersCard.ReferenceCardData);
        _playersCard.OnDeSpawn();
        _playersCard = null;
    }

    public void AddEnemyEffect(EnemyCardData card)
    {
        EnemyCard = card;     
    }

    public void ResolvePlayerEffects()
    {
        EffectManager.Instance.ActivateEffect(PlayersCard.ReferenceCardData.CardResolutionEffects);
    }

    //plays activates an ability based on the card type. in the future enemies will have a similar system to the players cards
    public void ResolveEnemyEffects()
    {
        EffectManager.Instance.ActivateEffect(EnemyCard.CardResolutionEffects);
    }

    //discards the played card
    public void CleanUpPhase()
    {
        CardGameManager.Instance.HandleCardDiscard(PlayersCard.ReferenceCardData);
        PlayersCard.OnDeSpawn();
        PlayersCard = null;
        CardGameManager.Instance.DrawPhaseStart();
    }
}
