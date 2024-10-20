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
            }
            else
            {
                return;
            }              
        }
    }
    private CardData _cardData;

    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _cardType;
    [SerializeField] private RawImage _image;

    [Header("UI intereaction settings")]
    [SerializeField] private float _hoverSizeIncrease = 1.25f;

     public bool InHand;
    [HideInInspector] public bool InTimeSlot;
    


    private void Awake()
    {
       
    }
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHover()
    {
        if (InHand)
        {
            transform.localScale = Vector3.one * _hoverSizeIncrease;
            GetComponent<Canvas>().sortingOrder += 1;
        }      
    }

    public void HoverExit()
    {
        if (InHand)
        {
            transform.localScale = Vector3.one;
            GetComponent<Canvas>().sortingOrder -= 1;
        }       
    }

    public void OnRightClick()
    {
        if (CardGameManager.Instance.CurrentPhase == Phase.PlayPhase)
        {
            if (InHand)
            {
                CardGameManager.Instance.EnergyGain(1);
                CardGameManager.Instance.HandleCardDiscard(this.ReferenceCardData);
                OnDeSpawn();
                Debug.Log("right");
            }
        }
    }
    //what the card does when its clicked
    public void OnCLick()
    {
        Debug.Log(CardGameManager.Instance.CurrentPhase);
        if(CardGameManager.Instance.CurrentPhase == Phase.PlayPhase)
        {
            if(InHand)
            {
                
                CardGameManager.Instance.PlayCard(this);
                transform.localScale = Vector3.one;
                
            }
            else if(InTimeSlot)
            {
                //GetComponentInParent<TimeSlot>().RemoveCard();
            }
        }
    }
}
