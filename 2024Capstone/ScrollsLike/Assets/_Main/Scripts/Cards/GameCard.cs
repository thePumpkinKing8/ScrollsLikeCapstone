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
            if (_cardData == null)
            {
                _cardData = value;
                EnergyCost = ReferenceCardData.EnergyCost;
            }
            else
            {
                return;
            }              
        }
    }

    private HandController _handController;
    private CardData _cardData;

    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _cardType;
    [SerializeField] private RawImage _image;

    [Header("UI intereaction settings")]
    [SerializeField] private float _hoverSizeIncrease = 1.25f;
    private Vector3 _baseSize;

    private bool _inHand;
    private bool _inTimeSlot;
    public int EnergyCost { get; private set; }

    private int _slotSortOrder = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        _description.text = _cardData.CardDescription;
        _title.text = _cardData.CardName;
        _cardType.text = _cardData.CardType.ToString();
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

    public void SetHandParent(HandController hand)
    {
        _handController = hand;
        _inHand = true;
        _inTimeSlot = false;
    }

    public void OnHover()
    {
        if (_inHand)
        {
            transform.localScale = Vector3.one * _hoverSizeIncrease;            
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
        
        //GetComponent<Canvas>().sortingOrder = _slotSortOrder;
    }

    public void OnRightClick()
    {
        if (CardGameManager.Instance.CurrentPhase == Phase.PlayPhase)
        {
            if (_inHand)
            {
                CardGameManager.Instance.DiscardForEnergy(this);
                CardGameManager.Instance.HandleCardDiscard(this.ReferenceCardData);
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
            else if(_inTimeSlot)
            {
                GetComponentInParent<TimeSlot>().RemoveCard(this);
                if(EnergyCost > 0)
                {
                    HealthManager.Instance.ChangeEnergy(EnergyCost);
                }
                SetOrder(1, true);
            }
        }
    }

    public void PlayCard()
    {
        if(EnergyCost > 0)
        {
            if (HealthManager.Instance.Energy > EnergyCost)
            {
                HealthManager.Instance.ChangeEnergy(-EnergyCost);
            }
            else
                return;
        }
        CardGameManager.Instance.PlayCard(this);
        transform.localScale = _baseSize;
        _inHand = false;
        _inTimeSlot = true;
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
