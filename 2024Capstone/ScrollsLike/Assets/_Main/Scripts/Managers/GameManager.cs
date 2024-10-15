using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Transform Player { get { return _player; } set { _player = value; } }
    private Transform _player;
    private EnemyDeck _opponent;
    private void Start()
    {
        DontDestroyOnLoad(this);
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
}
