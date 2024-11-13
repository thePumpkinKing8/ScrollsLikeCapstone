using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCard : PoolObject
{
    //public CardData TestCard;
    public CardData ReferenceCardData 
    { 
        get
        {
            return _cardData;
        }
        set
        {
            _cardData = value;
            SetUpCard();
        }
    }

    private HandController _handController;
    private CardData _cardData;

    public CardType CardsType
    {
        get
        {
            return ReferenceCardData.CardType;
        }
    }

    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _cardType;
    [SerializeField] private TextMeshProUGUI _energyCost;
    [SerializeField] private RawImage _image;

    [Header("UI intereaction settings")]
    [SerializeField] private float _hoverSizeIncrease = 1.25f;
    private Vector3 _baseSize;

    private bool _inHand;
    public int EnergyCost { get; private set; }

    private int _slotSortOrder = 1;
    
    // Start is called before the first frame update
    public void SetUpCard()
    {
        _description.text = _cardData.CardDescription;
        _title.text = _cardData.CardName;
        _cardType.text = _cardData.CardType.ToString();
        EnergyCost = _cardData.EnergyCost;
        _energyCost.text = _cardData.EnergyCost.ToString();
        if(_cardData.CardImage != null)
        {
            _image.texture = _cardData.CardImage;
        }
        GetComponent<Canvas>().sortingOrder = _slotSortOrder;
        _baseSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSize()
    {
        _baseSize = transform.localScale;
    }
    public void SetHandParent(HandController hand)
    {
        _handController = hand;
        _inHand = true;        
    }

    public void OnHover()
    {
        if (_inHand)
        {          
            transform.localScale = _baseSize * _hoverSizeIncrease;
        }
        SetOrder(14);
    }

    public void HoverExit()
    {
        if (_inHand)
        {           
            transform.localScale = _baseSize;
        }
        SetOrder(_slotSortOrder);
        
    }

    public void OnRightClick()
    {
        if (CardGameManager.Instance.CurrentPhase == Phase.PlayPhase)
        {
            if (_inHand)
            {
                CardGameManager.Instance.DiscardForEnergy(this);
                _handController.RemoveCard(this);
                OnDeSpawn();
                Debug.Log("right");
            }
        }
    }
    //what the card does when its clicked
    public void OnCLick()
    {
        if(CardGameManager.Instance.CurrentPhase == Phase.PlayPhase)
        {
            if(_inHand)
            {
                PlayCard();   
            }           
        }
    }

    public void PlayCard()
    {
        if(EnergyCost > 0)
        {
            if (HealthManager.Instance.Energy >= EnergyCost)
            {
                HealthManager.Instance.ChangeEnergy(-EnergyCost);
            }
            else
                return;
        }
        CardGameManager.Instance.PlayCard(this);
        transform.localScale = _baseSize;
        _inHand = false;
        
    }

    //refund spent energy if player decides to not play card
    public void CancelPlay()
    {
        HealthManager.Instance.ChangeEnergy(EnergyCost);
    }

    public void SetOrder(int num, bool changeDefault = false)
    {
        GetComponent<Canvas>().sortingOrder = num;
        if(changeDefault)
        {
            _slotSortOrder = num;
        }
    }
}
