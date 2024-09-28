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
    public GameCard EnemyCard { get; private set; } //placeholder for what the enemy will actually use

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard(GameCard card)
    {
        PlayersCard = card;
        card.transform.SetParent(transform, true);
        card.transform.position = this.transform.position;
        card.InHand = false;
        card.InTimeSlot = true;
    }

    public void RemoveCard()
    {
        CardGameManager.Instance.AddCardToHand(PlayersCard);
        _playersCard.OnDeSpawn();
        _playersCard = null;
       
    }

    public void AddEnemyEffect()
    {

    }

    public void ResolvePlayerEffects()
    {
        foreach(CardEffect effect in PlayersCard.ReferenceCardData.CardResolutionEffects)
        {
            //activate effect.
        }
    }

    public void ResolveEnemyEffects()
    {
        foreach (CardEffect effect in EnemyCard.ReferenceCardData.CardResolutionEffects)
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
