using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
   
    private void Start()
    {
        CardGameManager.Instance.Events.DrawPhaseStartEvent.AddListener(DrawPhaseStart);
        CardGameManager.Instance.Events.PrepPhaseStartEvent.AddListener(PrepPhaseStart);
        CardGameManager.Instance.Events.PlayPhaseStartEvent.AddListener(PlayPhaseStart);
        CardGameManager.Instance.Events.ResolutionPhaseStartEvent.AddListener(ResolutionPhaseStart);
        CardGameManager.Instance.Events.CleanupPhaseStartEvent.AddListener(CleanupPhaseStart);
    }

    public void DrawPhaseStart()
    {
        CardGameManager.Instance.DrawPhaseEnd();
    }

    public void PrepPhaseStart()
    {
        CardGameManager.Instance.PrepPhaseEnd();
    }

    public void PlayPhaseStart()
    {
        CardGameManager.Instance.PlayPhaseEnd();
    }

    public void ResolutionPhaseStart()
    {
        CardGameManager.Instance.ResolutionPhaseEnd();       
    }

    public void CleanupPhaseStart()
    {
        CardGameManager.Instance.CleanupPhaseEnd();
      
    }  
}
