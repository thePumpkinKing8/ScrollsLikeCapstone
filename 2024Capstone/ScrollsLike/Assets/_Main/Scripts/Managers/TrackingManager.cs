using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackingManager : Singleton<TrackingManager>
{
    public int StrikesPlayed { get; private set; } = 0;


    // Start is called before the first frame update
    void Start()
    {
        CardGameManager.Instance.Events.AttackPlayed.AddListener(StrikeIncrement);
        CardGameManager.Instance.Events.CleanupPhaseEndEvent.AddListener(ClearStats);
    }

    public void StrikeIncrement()
    {
        StrikesPlayed += 1;
    }

    public void ClearStats()
    {
        StrikesPlayed = 0;
    }

}
