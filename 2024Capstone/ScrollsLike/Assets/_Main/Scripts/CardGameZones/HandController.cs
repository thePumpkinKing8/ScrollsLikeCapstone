using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject CardPrefab;
    [Header("draw settings")]
    [Tooltip("time between cards drawn to hand")][SerializeField] private float _drawDelay;
    [SerializeField] private int _maxCardsInHand = 12;
    [SerializeField] private int _minCardsInHand = 5;
    [SerializeField] private int _startingHandSize = 8;

    private List<GameCard> _cardsInHand = new List<GameCard>(); 
    

    private void Awake()
    {
        CardGameManager.Instance.Events.CardDrawnEvent.AddListener(CardDrawn);
        CardGameManager.Instance.Events.DrawPhaseEndEvent.AddListener(DrawPhase);
        CardGameManager.Instance.Events.AddCardToHand.AddListener(AddCard);
        CardGameManager.Instance.Events.PlayCard.AddListener(RemoveCard);
    }
    
    //spawns a card in the players hand
    public void CardDrawn(CardData drawnCard)
    {
        GameCard newCard = PoolManager.Instance.Spawn("Card").GetComponent<GameCard>();
        newCard.transform.SetParent(transform, true);
        newCard.ReferenceCardData = drawnCard;       
        _cardsInHand.Add(newCard);
        newCard.InHand = true;
        newCard.InTimeSlot = false;
    }

    public void DrawPhase()
    {
        if(_cardsInHand.Count < _minCardsInHand)
        {
            StartCoroutine(DrawCards(_minCardsInHand - _cardsInHand.Count));
        }
    }

    public void GameStart()
    {
        StartCoroutine(DrawCards(_startingHandSize));
    }


    public void RemoveCard(GameCard card)
    {
        _cardsInHand.Remove(card);
    }

    public void AddCard(GameCard card)
    {
        CardDrawn(card.ReferenceCardData);
    }

    //draws the player multiple cards at the start if the draw phase
    IEnumerator DrawCards(int numberOfCards)
    {
        Debug.Log(numberOfCards);
        for(int i = 0; i < numberOfCards; i++)
        {
            Debug.Log("turn");
            CardGameManager.Instance.DrawCard();
            yield return new WaitForSeconds(_drawDelay);
        }
        CardGameManager.Instance.PrepPhaseStart();
        yield return null;
    }
}
