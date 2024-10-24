using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlot : MonoBehaviour
{
    public List<GameCard> PlayerCards
    {
        get { return _playersCards; }
        private set { _playersCards = value; }
    }
    private List<GameCard> _playersCards = new List<GameCard>();
    public EnemyCardData EnemyCard { get; private set; } 

    private bool _active = false;
    private bool _isPlaying = false;

    private void Awake()
    {
        CardGameManager.Instance.Events.EffectEnded.AddListener(CardResolved);
    }
    public void ToggleActive()
    {
        _active = !_active;
    }

    public void CardResolved()
    {
        _isPlaying = false;
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
        card.SetOrder(PlayerCards.Count, true);
    }

    public void RemoveCard(GameCard card)
    {
        CardGameManager.Instance.AddCardToHand(card);
        GameCard removedCard = PlayerCards.Find( x => x == card);
        removedCard.OnDeSpawn();
        PlayerCards.Remove(removedCard);
       
    }

    public void DiscardCard(GameCard card)
    {       
            CardGameManager.Instance.HandleCardDiscard(card.ReferenceCardData);
            card.OnDeSpawn();   
    }

    public void AddEnemyEffect(EnemyCardData card)
    {
        EnemyCard = card;     
    }

    public void ResolvePlayerEffects()
    {
        StartCoroutine("ResolvePlayer");
        
    }

    //plays activates an ability based on the card type. in the future enemies will have a similar system to the players cards
    public void ResolveEnemyEffects()
    {
        EffectManager.Instance.ActivateEffect(EnemyCard.CardResolutionEffects);
    }

    //discards the played card
    public void CleanUpPhase()
    {
        PlayerCards.Clear();
    }

    IEnumerator ResolvePlayer()
    {
        foreach(GameCard card in PlayerCards)
        {
            card.SetOrder(5);
            _isPlaying = true;
            EffectManager.Instance.ActivateEffect(card.ReferenceCardData.CardResolutionEffects);       
            yield return new WaitUntil(() => _isPlaying == false);
        }
        yield return null;
    }

}
