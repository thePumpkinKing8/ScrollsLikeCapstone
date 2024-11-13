using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoolsGuard", menuName = "StanceEffects/FoolsGuard")]
public class FoolsGuard : StanceTrigger
{
    private int _attacksPlayed = 0;
    protected override void SetUpTrigger()
    {
        base.SetUpTrigger();
        CardGameManager.Instance.Events.PlayerHit.AddListener(Trigger);
        CardGameManager.Instance.Events.CleanupPhaseStartEvent.AddListener(ClearCounter);
    }

    protected override void Trigger()
    {
        base.Trigger();
        Debug.Log("go off");
        foreach(CardEffect effect in _effectsFromTrigger)
        {
            ///effect.Modifier = _attacksPlayed;
        }
        EffectManager.Instance.ActivateEffect(_effectsFromTrigger);
    }

    public void AddToCounter()
    {
        _attacksPlayed++;
    }

    public void ClearCounter()
    {
        _attacksPlayed = 0;
    }

}
