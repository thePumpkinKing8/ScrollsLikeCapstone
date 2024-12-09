using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardMenu : MonoBehaviour
{
    [SerializeField] private GameObject discardMenu;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private GameObject restUI; // Reference to the rest UI
    private PlayerDeck playerDeck;
    private List<CardData> availableCards;

    void Start()
    {
        discardMenu.SetActive(false);
        playerDeck = GameManager.Instance.PlayersDeck;

        if (playerDeck == null)
        {
            Debug.LogError("PlayerDeck is not found!");
        }
    }

    public void OpenDiscardMenu()
    {
        if (playerDeck == null || playerDeck.Deck == null)
        {
            return;
        }


        /*
        discardMenu.SetActive(true);
        availableCards = new List<CardData>(playerDeck.Deck);

        //List<CardData> selectedCards = SelectUniqueCards(availableCards, 4);

        foreach (Transform child in cardContainer)
        {
            child.GetComponent<PoolObject>().OnDeSpawn();
        }

        foreach (var card in availableCards)
        {
            PoolObject cardObject = PoolManager.Instance.Spawn("Card");
            GameObject cardGO = cardObject.gameObject;
            GameCard cardUI = cardGO.GetComponent<GameCard>();



            if (cardUI == null)
            {
                Debug.LogError("UICard component is missing!");
                continue;
            }

            cardUI.ReferenceCardData = card;
            cardGO.transform.SetParent(cardContainer, false);
            cardUI.SetUpCard();
            cardGO.transform.localPosition = Vector3.zero;

            Button button = cardGO.GetComponent<Button>();
            if (button != null)
            {
                //button.onClick.AddListener(() => SelectCardForDiscard(cardUI));
            }
        }
        */
    }

    public void PopulateDeckUI(List<CardData> cardsToDisplay = null)
    {
        ClearExistingCards();



        if (GameManager.Instance != null)
        {
            List<CardData> deck = new List<CardData>();
            if (cardsToDisplay != null)
                deck = cardsToDisplay;
            else
                deck = GameManager.Instance.PlayersDeck.Deck;
            Debug.Log(deck.Count);
            foreach (CardData card in deck)
            {
                UICard gameCard = PoolManager.Instance.Spawn("UICard").GetComponent<UICard>();
                gameCard.ReferenceCardData = card;
                gameCard.transform.SetParent(cardContainer);
                gameCard.GetComponent<Canvas>().overrideSorting = false;

                Button button = gameCard.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => SelectCardForDiscard(gameCard));
                }
            }
        }
        else
        {
            Debug.LogWarning("GameManager instance not found in the scene.");
        }
    }
    private void ClearExistingCards()
    {
        foreach (PoolObject child in cardContainer.GetComponentsInChildren<PoolObject>())
        {
            child.OnDeSpawn();
        }
    }

    private void SelectCardForDiscard(UICard cardUI)
    {
        cardUI.ToggleSelected();
        Debug.Log($"Card selected: {cardUI.ReferenceCardData.CardName}, Selected: {cardUI.IsSelected()}");
    }

    private List<CardData> SelectUniqueCards(List<CardData> availableCards, int count)
    {
        List<CardData> selectedCards = new List<CardData>();
        HashSet<string> selectedCardNames = new HashSet<string>();
        List<CardData> uniqueCards = new List<CardData>();

        foreach (var card in availableCards)
        {
            if (!selectedCardNames.Contains(card.CardName))
            {
                selectedCardNames.Add(card.CardName);
                uniqueCards.Add(card);
            }
        }

        for (int i = 0; i < count && uniqueCards.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, uniqueCards.Count);
            CardData selectedCard = uniqueCards[randomIndex];
            selectedCards.Add(selectedCard);
            uniqueCards.RemoveAt(randomIndex);
        }

        return selectedCards;
    }

    public void ConfirmDiscard()
    {
        List<CardData> cardsToDiscard = new List<CardData>();

        foreach (Transform child in cardContainer)
        {
            UICard cardUI = child.GetComponent<UICard>();
            if (cardUI == null)
            {
                Debug.LogError("UICard component is missing!");
                continue;
            }

            if (cardUI.IsSelected())
            {
                cardsToDiscard.Add(cardUI.GetCardData());
                Debug.Log($"Card marked for discard: {cardUI.GetCardData().CardName}");
            }
        }

        foreach (var card in cardsToDiscard)
        {
            if (playerDeck != null)
            {
                playerDeck.RemoveCardFromDeck(card);
                Debug.Log($"Card discarded from deck: {card.CardName}");
            }
            else
            {
                Debug.LogError("PlayerDeck is null.");
            }
        }

        foreach (Transform child in cardContainer)
        {
            PoolManager.Instance.DeSpawn(child.GetComponent<UICard>());
        }

        CloseMenu();

        GameManager.Instance.HideRestUI();
    }

    private void CloseMenu()
    {
        BlakesAudioManager.Instance.PlayAudio("Button");
        discardMenu.SetActive(false);
    }
}
