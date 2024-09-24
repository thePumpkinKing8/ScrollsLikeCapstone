using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCard : MonoBehaviour
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


    private void Awake()
    {
       // ReferenceCardData = TestCard;
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
        transform.localScale = Vector3.one * _hoverSizeIncrease;
        Debug.Log("Size increase");
    }

    public void HoverExit()
    {
        transform.localScale = Vector3.one;
    }
}
