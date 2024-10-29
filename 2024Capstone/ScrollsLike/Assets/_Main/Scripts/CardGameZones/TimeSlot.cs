using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
    public void ToggleActive()
    {
        _active = !_active;
        var color = GetComponent<Image>();
        if (!_active)
            color.color = Color.gray;
        else
            color.color = Color.red;
    }

    public void CardResolved()
    {
        _isPlaying = false;
    }

    //adds a card to the timeslot 
    public void AddCard(GameCard card)
    {
        if(PlayerCards.Count >= 3)
        {
            CardGameManager.Instance.AddCardToHand(card);
        }
        else
        {
            PlayerCards.Add(card);
            card.transform.SetParent(transform, true);
            card.transform.position = this.transform.position;
            card.transform.localScale = Vector3.zero;
            card.SetOrder(PlayerCards.Count, true);
        }
        
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
    

    //discards the played card
    public void CleanUpPhase()
    {
        foreach(GameCard card in PlayerCards)
        {
            CardGameManager.Instance.DiscardCard(card.ReferenceCardData);
            card.OnDeSpawn();
        }
        PlayerCards.Clear();
    }

    IEnumerator ResolvePlayer()
    {
        foreach(GameCard card in PlayerCards)
        {
            card.SetOrder(5);
            _isPlaying = true;
            EffectManager.Instance.ActivateEffect(card.ReferenceCardData.CardResolutionEffects, this);       
            yield return new WaitUntil(() => _isPlaying == false);
        }
        _isPlaying = true;
        EffectManager.Instance.ActivateEffect(EnemyCard.CardResolutionEffects, this);
        yield return new WaitUntil(() => _isPlaying == false);
        CardGameManager.Instance.MoveToNext();
        CardGameManager.Instance.ResolveSlot();
        yield return null;
    }

}
