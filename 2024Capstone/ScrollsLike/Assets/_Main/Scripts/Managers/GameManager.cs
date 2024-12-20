using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState State { get; private set; }
    public Transform Player { get { return _player; } set { _player = value; } }
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
    private GameObject _enemyRef;
    public int LevelIndex { get { return _levelIndex; } }
    private int _levelIndex = 0;

    [SerializeField] private RewardScreen _rewardScreen;
    [SerializeField] private GameObject restUIPrefab;

    public bool LevelActive { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        WoundsRemaining = _maxWounds;
        HealthRemaining = _maxHealth;
        LevelActive = true;
        State = GameState.Dungeon;
    }

    private void Start()
    {
        PlayersDeck.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerWins();
        }
    }

    public void GoToCombat(EnemyDeck opponent, GameObject obj)
    {
        if (State == GameState.CardGame)
            return;
        _enemyRef = obj;
        _opponent = opponent;
        _playerPosition = Player.position;
        State = GameState.CardGame;
        SceneManager.LoadScene("CardGame", LoadSceneMode.Additive);
    }

    public void CardGameStart()
    {
        EnemyManager.Instance.SetUp(_opponent);
    }

    public void NextLevel()
    {
        _levelIndex++;
        if (_levelIndex >= 5)
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
        PoolManager.Instance.ClearPool();
        Destroy(_enemyRef);
    }

    public void PlayerLoses()
    {
        State = GameState.Dead;
        SceneManager.LoadScene("LoseScreen");
        Destroy(gameObject);
    }

    public void CardRewards()
    {
        LevelActive = false;
        _rewardScreen.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        LevelActive = true;
        State = GameState.Dungeon;
        PoolManager.Instance.ClearPool();
    }

    public void SetPause()
    {
        State = GameState.Pause;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
    }

    public void ShowRestUI()
    {
        if (restUIPrefab != null)
        {
            restUIPrefab.SetActive(true);
            SetPause(); 
        }
    }

    public void HideRestUI()
    {
        if (restUIPrefab != null)
        {
            restUIPrefab.SetActive(false);
            ResumeGame();
        }
    }
}

public enum GameState
{
    Dungeon,
    CardGame,
    Pause,
    Dead
}
