using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlot : MonoBehaviour
{
    private List<GameCard> _playersCards = new List<GameCard>();
    public EnemyCard EnemyData { get; private set; } 

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

    public void CleanUpPhase()
    {
        if(EnemyData.ReferenceCardData.CardOnDiscardEffects.Count > 1)
        {
            EffectManager.Instance.ActivateEffect(EnemyData.ReferenceCardData.CardOnDiscardEffects);
            EnemyData.OnDeSpawn();
            EnemyData = null;
        }
    }

    public void CardResolved()
    {
        _isPlaying = false;
    }


    public void AddEnemyEffect(EnemyCardData card)
    {
        EnemyData =  PoolManager.Instance.Spawn("EnemyCard").GetComponent<EnemyCard>(); 
        EnemyData.transform.SetParent(transform);
        EnemyData.ReferenceCardData = card;
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
