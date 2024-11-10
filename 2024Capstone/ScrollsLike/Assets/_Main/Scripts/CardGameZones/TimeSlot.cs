using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlot : MonoBehaviour
{
    private List<GameCard> _playersCards = new List<GameCard>();
    public EnemyCardData EnemyCard { get; private set; } 

    public bool Active{ get; private set; } // whether this slot can be targeted or not

    public int SlotHealth {get; private set; }
    
    private bool _isPlaying = false;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
    }
    public void ToggleActive()
    {
        Active = Active;
        var color = GetComponent<Image>();
        if (Active)
            color.color = Color.gray;
        else
           color.color = Color.white;
    }

    public void CardResolved()
    {
        _isPlaying = false;
    }


    public void AddEnemyEffect(EnemyCardData card)
    {
        EnemyCard = card;
        var cardOBJ =  PoolManager.Instance.Spawn("Card"); //needs to be a different prefab for enemy cards. this is just for testing
        cardOBJ.transform.SetParent(transform);
    }

    public void EnemyHit(int damage)
    {
        SlotHealth -= damage;
        if(SlotHealth <= 0)
        {
            Active = false;
        }
    }

    public void EnemyHeal(int value)
    {
        SlotHealth += value;
    }

   
}
