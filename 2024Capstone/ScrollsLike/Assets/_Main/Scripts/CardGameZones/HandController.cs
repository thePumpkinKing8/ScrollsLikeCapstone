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
    }
    public void CardDrawn(CardData drawnCard)
    {
        GameCard newCard = Instantiate(CardPrefab).GetComponent<GameCard>();
        newCard.transform.SetParent(transform, true);
        newCard.ReferenceCardData = drawnCard;       
        _cardsInHand.Add(newCard);
    }

    public void DrawPhase()
    {
        if(_cardsInHand.Count < _minCardsInHand)
        {
            StartCoroutine(DrawCards(_minCardsInHand - _cardsInHand.Count));
        }
    }

    IEnumerator DrawCards(int numberOfCards)
    {
        Debug.Log(numberOfCards);
        for(int i = 0; i < numberOfCards; i++)
        {
            CardGameManager.Instance.DrawCard();
            yield return new WaitForSeconds(_drawDelay);
        }
        CardGameManager.Instance.PrepPhaseStart();
        yield return null;
    }
}
