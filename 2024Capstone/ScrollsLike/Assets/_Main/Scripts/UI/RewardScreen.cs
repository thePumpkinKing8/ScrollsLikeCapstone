using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreen : MonoBehaviour
{
    [SerializeField] private Transform _cardSpot;

    private CardData _selectedCard;
    private UICard[] rewards = new UICard[3];

    private void OnEnable()
    {
        Time.timeScale = 1;
        _selectedCard = null;
        var cards = Resources.LoadAll<CardData>("Cards");

        for (int i = 0; i < 3; i++)
        {
            rewards[i] = PoolManager.Instance.Spawn("UICard").GetComponent<UICard>();
            rewards[i].ReferenceCardData = cards[Random.Range(0, cards.Length)];
            rewards[i].transform.SetParent(_cardSpot);
            rewards[i].transform.position = _cardSpot.position;
            rewards[i].SetScreenRef(this);

            // Adding button click listener for each card
            Button button = rewards[i].GetComponent<Button>();
            if (button != null)
            {
                int index = i;  // Capture the index for the callback
                button.onClick.AddListener(() => CardSelect(rewards[index].GetCardData()));
            }
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Start()
    {
        //gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        foreach (UICard card in rewards)
        {
            card.OnDeSpawn();
        }
    }

    public void SkipReward()
    {
        gameObject.SetActive(false);
    }

    public void CardSelect(CardData card)
    {
        if (_selectedCard == card) // If the same card is clicked again, deselect it
        {
            _selectedCard = null;
            Debug.Log(Time.timeScale);
        }
        else
        {
            _selectedCard = card;
            Debug.Log($"Card selected: {card.CardName}");
        }

        // Update visual feedback
        foreach (var cardUI in rewards)
        {
            cardUI.SetSelected(cardUI.GetCardData() == _selectedCard);
        }
    }

    public void ComfirmReward()
    {
        Debug.Log("cock");
        if (_selectedCard != null)
        {
            GameManager.Instance.PlayersDeck.AddCardToDeck(_selectedCard);
            Debug.Log($"Card {_selectedCard.CardName} added to deck");
        }
        else
        {
            Debug.Log("No card selected!");
        }

        gameObject.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
