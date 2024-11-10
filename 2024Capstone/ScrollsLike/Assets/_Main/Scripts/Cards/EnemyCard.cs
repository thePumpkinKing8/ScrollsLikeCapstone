using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EnemyCard : PoolObject
{
    public EnemyCardData ReferenceCardData
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

    private EnemyCardData _cardData;

    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _cardType;
    [SerializeField] private RawImage _image;

    [Header("UI intereaction settings")]
    [SerializeField] private float _hoverSizeIncrease = 1.25f;
    private Vector3 _baseSize;
    // Start is called before the first frame update
    void Start()
    {
        _description.text = _cardData.CardDescription;
        _title.text = _cardData.CardName;
        _cardType.text = _cardData.CardType.ToString();
        if (_cardData.CardImage != null)
        {
            _image.texture = _cardData.CardImage;
        }
        _baseSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHover()
    {  
        transform.localScale = Vector3.one * _hoverSizeIncrease;        
    }

    public void OnHoverExit()
    {
        transform.localScale = _baseSize;
    }
}
