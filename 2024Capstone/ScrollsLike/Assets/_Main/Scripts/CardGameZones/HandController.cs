using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : Singleton<HandController>
{
    [SerializeField] private GameObject CardPrefab;
    [Header("draw settings")]
    [Tooltip("time between cards drawn to hand")][SerializeField] private float _drawDelay;
    [SerializeField] public int MaxCardsInHand { get; private set; } = 12;
    [SerializeField] private int _cardsDrawnPerTurn = 3;
    [SerializeField] private int _startingHandSize = 8;
    public int DrawMod { get; set; } = 0;

    public List<GameCard> CardsInHand { get { return _cardsInHand; } }

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
        BlakesAudioManager.Instance.PlayAudio("CardDraw");
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
        if(_cardsInHand.Count < MaxCardsInHand)
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

    public void AddCardFromData(CardData card)
    {
        CardDrawn(card);
    }

    private void SetHandOrder()
    {
        int i = 1;
        foreach (GameCard card in _cardsInHand)
        {            
            card.SetOrder(i);
            card.SetHandParent(this);
            i++;
        }
    }

    //draws the player multiple cards at the start if the draw phase
    IEnumerator DrawCards(int numberOfCards, bool drawPhase = false)
    {
        Debug.Log(numberOfCards + DrawMod);
        Debug.Log($"Number of cards in hand is {_cardsInHand.Count}");
        for(int i = 0; i < numberOfCards + DrawMod; i++)
        {

            if (_cardsInHand.Count < MaxCardsInHand)
            {
                CardGameManager.Instance.DrawCard();
                yield return new WaitForSeconds(_drawDelay);
            }
            else
            {
                Debug.Log("To Many Cards");
                break;
            }
                
        }
        SetHandOrder();
        if(drawPhase)
            CardGameManager.Instance.PrepPhaseStart();
        DrawMod = 0;
        Debug.Log($"Number of cards in hand, after drawing, is {_cardsInHand.Count}");
        yield return null;
    }
}
