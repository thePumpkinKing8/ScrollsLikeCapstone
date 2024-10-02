using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthManager : Singleton<HealthManager>
{
    [SerializeField] private int _startingHealth; //will be set by seperate script later
    public int Wounds = 3;
    public int PlayerHealth { get; set; }
    public int EnemyHealth { get; private set; }
    private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _enemyHealthText;

    [HideInInspector] public bool PlayerBlock;
    [HideInInspector] public bool EnemyBlock;

    // Start is called before the first frame update
    void Awake()
    {
        CardGameManager.Instance.Events.PlayerHit.AddListener(PlayerHit);
        CardGameManager.Instance.Events.EnemyHit.AddListener(EnemyHit);
        PlayerHealth = _startingHealth;
        EnemyHealth = _startingHealth;
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth <= 0)
        {
            Wounds--;
            if (Wounds <= 0)
            {
                Debug.Log("Lose");
            }
            else
                PlayerHealth = _startingHealth;
        }
        else if (EnemyHealth <= 0)
        {
            Debug.Log("Win");
        }

        _text.text = $"Health:{PlayerHealth.ToString()} \nWounds:{Wounds.ToString()}";
        _enemyHealthText.text = $"Enemy Health:{EnemyHealth.ToString()}";

        if(PlayerHealth > _startingHealth)
        {
            PlayerHealth = _startingHealth;
        }
        
    }

    public void PlayerHit(int damage)
    {
        if(!PlayerBlock)
            PlayerHealth -= damage;
        else
            PlayerBlock = false;
    }
    public void EnemyHit(int damage)
    {
        if (!EnemyBlock)
            EnemyHealth -= damage;
        else
        {
            EnemyBlock = false;
            EnemyHealth -= Mathf.RoundToInt(damage / 2);
        }
            
    }
}
