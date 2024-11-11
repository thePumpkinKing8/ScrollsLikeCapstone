using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlot : MonoBehaviour
{
   
    public EnemyCard EnemyData { get; private set; } 

    public bool Active{ get; private set; } // whether this slot can be targeted or not

    private int _maxHealth;
    public int SlotHealth {get; private set; }
    
    //used for waiting for a cards effect to finish playing before continuing
    private bool _isPlaying = false;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        Active = true;
    }
    public void ToggleActive()
    {
        Active = !Active;
        var color = GetComponent<Image>();
        if (Active)
            color.color = Color.gray;
        else
           color.color = Color.white;
    }

    public void SetUp(int health)
    {
        _maxHealth = health;
        text.text = _maxHealth.ToString();
        SlotHealth = health;

    }

    public void ClearSlot()
    {
        EnemyData.OnDeSpawn();
        EnemyData = null;
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
        
        text.text = SlotHealth.ToString();
        if (SlotHealth <= 0)
        {
            ToggleActive();
            ClearSlot();
            text.text = "dead";
        }
    }

    public void EnemyHeal(int value)
    {
        if(!Active)
        {
            ToggleActive();
        }
        SlotHealth += value;
        if(SlotHealth > _maxHealth)
        {
            SlotHealth = _maxHealth;
        }
        text.text = SlotHealth.ToString();
    }

    public void ResolveEnemyEffect()
    {
        EffectManager.Instance.ActivateEffect(EnemyData.ReferenceCardData.CardResolutionEffects);
    }
   
}
