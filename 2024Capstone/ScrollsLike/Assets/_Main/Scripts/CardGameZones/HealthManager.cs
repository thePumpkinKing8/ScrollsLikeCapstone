using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//Should probably be changed to player manager
public class HealthManager : Singleton<HealthManager>
{
    [SerializeField] private int _startingHealth; //will be set by seperate script later
    public int Wounds = 3;
    public int PlayerHealth { get; set; }

    private TextMeshProUGUI _text;
    public int Energy { get; private set; }

    [HideInInspector] public int PlayerBlock { get; private set; }
    //[HideInInspector] public bool EnemyBlock;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        CardGameManager.Instance.Events.PlayerHit.AddListener(PlayerHit);
        CardGameManager.Instance.Events.PlayerGainsBlock.AddListener(GainBlock);
        CardGameManager.Instance.Events.EnergyChange.AddListener(ChangeEnergy);
        Energy = 0;
        PlayerHealth = _startingHealth;
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
                StartCoroutine(EndGame("Lose"));
            }
            else
            {
                var overDamage = PlayerHealth; //the damage that knocked the player below 0
                PlayerHealth = _startingHealth;
                PlayerHealth -= overDamage;
            }
                
        }

        _text.text = $"Health:{PlayerHealth.ToString()} \nWounds:{Wounds.ToString()}";

        if(PlayerHealth > _startingHealth)
        {
            PlayerHealth = _startingHealth;
        }
        
    }
    //deals health damage to the player
    public void PlayerHit(int damage)
    {
        if(PlayerBlock >= 0)
        {
            if(damage > PlayerBlock)
            {
                int remainder = damage - PlayerBlock;
                PlayerBlock = 0;
                PlayerHealth -= remainder;
            }
            else
            {
                PlayerBlock -= damage;
            }
        }
        else
        {
            PlayerHealth -= damage;
        }
    }

    public void GainBlock(int amount)
    {
        PlayerBlock += amount;
    }

    public void ChangeEnergy(int amount)
    {
        Energy += amount;
    }
    //Ends the game and returns to the adventure sections
    IEnumerator EndGame(string message)
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Anna_Gym");
        yield return null;
    }
}
