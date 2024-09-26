using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject CardPrefab;
    private List<GameCard> _cardsInHand = new List<GameCard>();

    private void Awake()
    {
        CardGameManager.Instance.Events.CardDrawnEvent.AddListener(CardDrawn);
    }
    public void CardDrawn(CardData drawnCard)
    {
        GameCard newCard = Instantiate(CardPrefab).GetComponent<GameCard>();
        newCard.transform.SetParent(transform, true);
        newCard.ReferenceCardData = drawnCard;       
    }
}
