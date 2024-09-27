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
        StartCoroutine(DrawPhase());
    }

    public void PrepPhaseStart()
    {
        StartCoroutine(PrepPhase());
    }

    public void PlayPhaseStart()
    {
        StartCoroutine(PlayPhase());
    }

    public void ResolutionPhaseStart()
    {
        StartCoroutine(ResolutionPhase());
    }

    public void CleanupPhaseStart()
    {
        StartCoroutine(CleanupPhase());
    }

    IEnumerator DrawPhase()
    {
        //handle any effects that happen here
        CardGameManager.Instance.DrawPhaseEnd();
        yield return null;
    }

    IEnumerator PrepPhase()
    {
        yield return null;
    }

    IEnumerator PlayPhase()
    {
        yield return null;
    }

    IEnumerator ResolutionPhase()
    {
        yield return null;
    }

    IEnumerator CleanupPhase()
    {
        yield return null;
    }
}
