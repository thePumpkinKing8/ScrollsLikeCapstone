using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICard : PoolObject
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

    private CardData _cardData;

    private bool _selected;

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

    private RewardScreen _rewardScreen;

    
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
        _baseSize = transform.localScale;
        _rewardScreen = GetComponentInParent<RewardScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSize()
    {
        _baseSize = transform.localScale;
    }


    public void OnHover()
    {       
        transform.localScale = _baseSize * _hoverSizeIncrease;
    }

    public void HoverExit()
    {                
        transform.localScale = _baseSize;    
    }

    //what the card does when its clicked
    public void OnCLick()
    {
        _rewardScreen.CardSelect(ReferenceCardData);
    }

    public void Select()
    {
        _selected = true;
    }

    public void DeSelect()
    {
        _selected = false;
    }

    public override void OnDeSpawn()
    {
        base.OnDeSpawn();
    }
}
