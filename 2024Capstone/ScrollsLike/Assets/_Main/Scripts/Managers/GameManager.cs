using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameState State 
    {
        get 
        {
            return _state;
        }
        private set 
        { 
            _state = value;
            switch(value)
            {
                case GameState.Dungeon:
                    BlakesAudioManager.Instance.PlayMusic("DungeonBGM");
                    break;
                case GameState.CardGame:
                    BlakesAudioManager.Instance.PlayMusic("CombatBGM");
                    break;
                case GameState.Rest:
                    BlakesAudioManager.Instance.PlayMusic("RestBGM");
                    break;
                case GameState.Dead:
                    BlakesAudioManager.Instance.PlayMusic("LoseBGM");
                    break;
            }
        } 
    }

    private GameState _state;
    public Transform Player { get { return _player; } set { _player = value; } }
    private Transform _player;

    public Vector3 PlayerPosition { get { return _playerPosition; } set { _playerPosition = value; } }
    private Vector3 _playerPosition;
    public int WoundsRemaining { get; private set; }
    [SerializeField] private int _maxWounds = 3;

    public int HealthRemaining { get; private set; }
    [SerializeField] private int _maxHealth = 25;
    public PlayerDeck PlayersDeck { get { return _playerDeck; } }
    [SerializeField] private PlayerDeck _playerDeck;
    [SerializeField] private EnemyDeck _opponent;
    private GameObject _enemyRef;
    public int LevelIndex { get { return _levelIndex; } }
    private int _levelIndex = 0;

    [SerializeField] private GameObject _rewardScreen;
    [SerializeField] private GameObject restUIPrefab;

    public bool LevelActive { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        WoundsRemaining = _maxWounds;
        HealthRemaining = _maxHealth;
        LevelActive = true;
        
    }

    private void Start()
    {
        _rewardScreen = FindObjectOfType<RewardScreen>().gameObject;
        _rewardScreen.SetActive(false);
        restUIPrefab = FindObjectOfType<RestUI>().gameObject;
        PlayersDeck.Initialize();
        State = GameState.Dungeon;
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

    public void EndRest()
    {

    }

    public void CardGameStart()
    {
        EnemyManager.Instance.SetUp(_opponent);
        HealthManager.Instance.PlayerHealth = HealthRemaining;
    }

    public void NextLevel()
    {
        _levelIndex++;
        if (_levelIndex > 2)
        {
            Debug.Log("Game won");
            SceneManager.LoadScene("EndScreen");
        }
        else
        {
            DungeonLevelLoader levelLoader = FindObjectOfType<DungeonLevelLoader>();
            if (levelLoader != null)
            {
                levelLoader.LoadNextLevel();
            }
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
        Destroy(gameObject,1);
    }

    public void CardRewards()
    {
        State = GameState.Rewards;
        LevelActive = false;
        _rewardScreen.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        LevelActive = true;
        State = GameState.Dungeon;
        Time.timeScale = 1f; 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
        PoolManager.Instance.ClearPool();
    }


    public void SetPause()
    {
        State = GameState.Pause;
       // Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
    }

    public void ShowRestUI()
    {
        if (restUIPrefab != null)
        {
            restUIPrefab.SetActive(true);
            State = GameState.Rest;
            SetPause(); 
        }
    }

    public void HideRestUI()
    {
        if (restUIPrefab != null)
        {
            restUIPrefab.SetActive(false);
            NextLevel();
            ResumeGame();
        }
    }

    public void FullyRegenerateHealth()
    {
        HealthRemaining = _maxHealth;
        Debug.Log("Player health fully regenerated.");
    }
}

public enum GameState
{
    Dungeon,
    CardGame,
    Pause,
    Rest,
    Rewards,
    Dead
}
