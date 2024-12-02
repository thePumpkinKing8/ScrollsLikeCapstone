using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScreen : MonoBehaviour
{
    [SerializeField] private Transform _cardSpot;

    private CardData _selectedCard;

    private UICard[] rewards = new UICard[3];

    private void OnEnable()
    {     
        _selectedCard = null;
        var cards = Resources.LoadAll<CardData>("Cards");

        for (int i = 0; i < 3; i++)
        {
            rewards[i] = PoolManager.Instance.Spawn("UICard").GetComponent<UICard>();
            rewards[i].ReferenceCardData = cards[Random.Range(0, cards.Length)];
            rewards[i].transform.SetParent(_cardSpot);
            rewards[i].transform.position = _cardSpot.position;
            rewards[i].SetScreenRef(this);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        Debug.Log("clicked");
        if(_selectedCard == card) //if a card is clicked a second time this deselects that card
        {
            _selectedCard = null;
        }

        else
            _selectedCard = card;
    }

    public void ComfirmReward()
    {
        if(_selectedCard != null)
            GameManager.Instance.PlayersDeck.AddCardToDeck(_selectedCard);
        
        gameObject.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
