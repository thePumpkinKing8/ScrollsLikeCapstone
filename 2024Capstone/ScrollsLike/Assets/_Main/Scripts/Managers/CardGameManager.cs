using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CardGameManager : Singleton<CardGameManager>
{
    public CardEventData Events { get { return _cardEventData; } }
    [SerializeField] private CardEventData _cardEventData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
