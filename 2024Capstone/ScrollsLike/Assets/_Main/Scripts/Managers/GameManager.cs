using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState State { get; private set; }
    public Transform Player { get { return _player; } set { _player = value;}}
    private Transform _player;

    public Vector3 PlayerPosition { get { return _playerPosition; } set { _playerPosition = value; } }
    private Vector3 _playerPosition;
    public int WoundsRemaining { get; private set; }
    [SerializeField] private int _maxWounds = 3;

    public int HealthRemaining { get; private set; }
    [SerializeField] private int _maxHealth = 14;
    public PlayerDeck PlayersDeck { get { return _playerDeck; } }
    [SerializeField] private PlayerDeck _playerDeck;
    [SerializeField] private EnemyDeck _opponent;
    public int LevelIndex { get { return _levelIndex; } }
    private int _levelIndex = 0;

    [SerializeField] private RewardScreen _rewardScreen;

    public bool LevelActive { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        PlayersDeck.Initialize();
        WoundsRemaining = _maxWounds;
        HealthRemaining = _maxHealth;
        LevelActive = true;
        State = GameState.Dungeon;
    }

    public void GoToCombat(EnemyDeck opponent)
    {
        if (State == GameState.CardGame)
            return;
        _opponent = opponent;
        _playerPosition = Player.position;
        SceneManager.LoadScene("CardGame", LoadSceneMode.Additive);
        State = GameState.CardGame;
    }
    
    public void CardGameStart()
    {
        EnemyManager.Instance.SetUp(_opponent);      
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
        Scene scene = SceneManager.GetSceneByName("CardGame");
        SceneManager.UnloadSceneAsync(scene);
        CardRewards();
    }

    public void PlayerLoses()
    {
        SceneManager.LoadScene("Anna_Gym");
    }

    public void CardRewards()
    {
        LevelActive = false;
        _rewardScreen.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        LevelActive = true;
    }
}

public enum GameState
{ 
    Dungeon,
    CardGame,
    Pause
}

