using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject CardPrefab;

    private void Awake()
    {
        CardGameEventManager.Instance.CardDrawnEvent.AddListener(DrawACard);
    }
    public void DrawACard(CardData drawnCard)
    {
        GameCard newCard = Instantiate(CardPrefab).GetComponent<GameCard>();
        newCard.transform.SetParent(gameObject.GetComponentInChildren<HorizontalLayoutGroup>().transform, true);
        newCard.ReferenceCardData = drawnCard;
        Debug.Log("dadw");
       
    }
}
