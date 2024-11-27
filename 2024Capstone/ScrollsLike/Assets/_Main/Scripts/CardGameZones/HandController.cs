using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : Singleton<HandController>
{
    [SerializeField] private GameObject CardPrefab;
    [Header("draw settings")]
    [Tooltip("time between cards drawn to hand")][SerializeField] private float _drawDelay;
    [SerializeField] private int _maxCardsInHand = 12;
    [SerializeField] private int _cardsDrawnPerTurn = 3;
    [SerializeField] private int _startingHandSize = 8;

    private List<GameCard> _cardsInHand = new List<GameCard>(); 
    

    protected override void Awake()
    {
        base.Awake();
        _drawDelay = CardGameManager.Instance.DrawDelay;
        
        CardGameManager.Instance.Events.CardDrawnEvent.AddListener(CardDrawn);
        CardGameManager.Instance.Events.AddCardToHand.AddListener(AddCard);
        CardGameManager.Instance.Events.PlayCard.AddListener(RemoveCard);
    }
    
    //spawns a card in the players hand
    public void CardDrawn(CardData drawnCard)
    {
        GameCard newCard = PoolManager.Instance.Spawn("Card").GetComponent<GameCard>();
        newCard.transform.SetParent(transform, true);
        newCard.SetSize();
        newCard.ReferenceCardData = drawnCard;       
        _cardsInHand.Add(newCard);
        SetHandOrder();
    }

    public void GameStart()
    {
        StartCoroutine(DrawCards(_startingHandSize,true));
    }

    public void DrawPhase()
    {
        if(_cardsInHand.Count < _maxCardsInHand)
        {
            StartCoroutine(DrawCards(_cardsDrawnPerTurn, true));
        }
    }

    public void DrawFromEffect(int numberOfCards)
    {
        StartCoroutine(DrawCards(numberOfCards));
    }

    public void RemoveCard(GameCard card)
    {
        _cardsInHand.Remove(card);
    }

    public void AddCard(GameCard card)
    {
        CardDrawn(card.ReferenceCardData);
    }

    private void SetHandOrder()
    {
        int i = 1;
        foreach (GameCard card in _cardsInHand)
        {            
            card.SetOrder(i, true);
            card.SetHandParent(this);
            i++;
        }
    }

    //draws the player multiple cards at the start if the draw phase
    IEnumerator DrawCards(int numberOfCards, bool drawPhase = false)
    {
        Debug.Log(numberOfCards);
        for(int i = 0; i < numberOfCards; i++)
        {

            if (_cardsInHand.Count < _maxCardsInHand)
            {
                CardGameManager.Instance.DrawCard();
                yield return new WaitForSeconds(_drawDelay);
            }
            else
                break;
        }
        SetHandOrder();
        if(drawPhase)
            CardGameManager.Instance.PrepPhaseStart();
        yield return null;
    }
}
