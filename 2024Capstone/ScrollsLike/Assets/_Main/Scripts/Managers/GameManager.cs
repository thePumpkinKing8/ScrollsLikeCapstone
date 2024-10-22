using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Transform Player { get { return _player; } set { _player = value;}}
    private Transform _player;
    public PlayerDeck PlayersDeck { get { return _playerDeck; } }
    [SerializeField] private PlayerDeck _playerDeck;
    private EnemyDeck _opponent;
    public int LevelIndex { get { return _levelIndex; } }
    private int _levelIndex = 0;

    private void Start()
    {
        DontDestroyOnLoad(this);
        PlayersDeck.Initialize();
    }

    public void GoToCombat(EnemyDeck opponent)
    {
        _opponent = opponent;
        SceneManager.LoadScene("CardGameTest");
    }
    
    public void CardGameStart()
    {
        EnemyManager.Instance.OpponentsDeck = _opponent;
    }

    public void NextLevel()
    {
        _levelIndex++;
        if(_levelIndex >= 5)
        {
            Debug.Log("Game won");
        }
    }
}
