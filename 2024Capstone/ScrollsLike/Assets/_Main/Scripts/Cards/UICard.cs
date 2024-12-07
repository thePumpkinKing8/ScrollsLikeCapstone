using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICard : PoolObject
{
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
    [SerializeField] private Image _typeImage;
    [Header("Type Images")]
    [SerializeField] private Sprite _strikeImage;
    [SerializeField] private Sprite _guardImage;
    [SerializeField] private Sprite _stanceImage;

    [SerializeField] private TextMeshProUGUI _energyCost;
    [SerializeField] private RawImage _image;

    [Header("UI interaction settings")]
    [SerializeField] private float _hoverSizeIncrease = 1.25f;
    private Vector3 _baseSize;

    public int EnergyCost { get; private set; }

    private RewardScreen _rewardScreen;

    public void SetUpCard()
    {
        _description.text = _cardData.CardDescription;
        _title.text = _cardData.CardName;
        _cardType.text = _cardData.CardType.ToString();
        switch (_cardData.CardType)
        {
            case CardType.Strike:
                _typeImage.sprite = _strikeImage;
                break;
            case CardType.Guard:
                _typeImage.sprite = _guardImage;
                break;
            case CardType.Stance:
                _typeImage.sprite = _stanceImage;
                break;
        }
        EnergyCost = _cardData.EnergyCost;
        _energyCost.text = _cardData.EnergyCost.ToString();
        if (_cardData.CardImage != null)
        {
            _image.texture = _cardData.CardImage;
        }
        _baseSize = transform.localScale;
    }

    public void SetScreenRef(RewardScreen screen)
    {
        _rewardScreen = screen;
    }

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
        if (!_selected)
            transform.localScale = _baseSize;
    }

    //what the card does when its clicked
    public void OnClick()
    {
        _rewardScreen.CardSelect(ReferenceCardData);
        _selected = true;
    }

    public void DeSelect()
    {
        _selected = false;
        transform.localScale = _baseSize;
    }

    public bool IsSelected()
    {
        return _selected;
    }

    public void ToggleSelected()
    {
        if (_selected)
        {
            DeSelect();
        }
        else
        {
            _selected = true; 
            transform.localScale = _baseSize * _hoverSizeIncrease; 
        }
    }

    public CardData GetCardData()
    {
        return _cardData;
    }

    public override void OnDeSpawn()
    {
        base.OnDeSpawn();
    }
}
