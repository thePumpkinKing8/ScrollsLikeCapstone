using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Transform Player { get { return _player; } set { _player = value;}}
    private Transform _player;

    public int WoundsRemaining { get; private set; }
    [SerializeField] private int _maxWounds = 3;

    public int HealthRemaining { get; private set; }
    [SerializeField] private int _maxHealth = 14;
    public PlayerDeck PlayersDeck { get { return _playerDeck; } }
    [SerializeField] private PlayerDeck _playerDeck;
    private EnemyDeck _opponent;
    public int LevelIndex { get { return _levelIndex; } }
    private int _levelIndex = 0;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        PlayersDeck.Initialize();
        WoundsRemaining = _maxWounds;
        HealthRemaining = _maxHealth;
    }

    public void GoToCombat(EnemyDeck opponent)
    {
        _opponent = opponent;
        SceneManager.LoadScene("CardGame");
    }
    
    public void CardGameStart()
    {
        if(EnemyManager.Instance.OpponentsDeck == null)
        {
            EnemyManager.Instance.OpponentsDeck = _opponent;
        }
        
    }

    public void NextLevel()
    {
        _levelIndex++;
        if(_levelIndex >= 5)
        {
            Debug.Log("Game won");
        }
    }

    public void PlayerWins()
    {
        HealthRemaining = HealthManager.Instance.PlayerHealth;
        WoundsRemaining = HealthManager.Instance.Wounds;
        SceneManager.LoadScene("Anna_Gym");
    }

    public void PlayerLoses()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
