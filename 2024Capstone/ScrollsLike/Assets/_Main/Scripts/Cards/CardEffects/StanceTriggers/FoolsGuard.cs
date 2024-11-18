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
        ClearCounter();
        CardGameManager.Instance.Events.PlayerHit.AddListener(Trigger);
        CardGameManager.Instance.Events.CleanupPhaseStartEvent.AddListener(ClearCounter);
        CardGameManager.Instance.Events.AttackPlayed.AddListener(AddToCounter);
    }

    protected override void Trigger()
    {
        if (!_cardsData.IsActive)
            return;
        base.Trigger();
        Debug.Log("go off");
        foreach(CardEffect effect in _effectsFromTrigger)
        {
            foreach(CardEffector effector in effect.CardEffectors)
            {
                if(effector.Strategy is FoolsGuardStrategy)
                {
                    FoolsGuardStrategy strat = (FoolsGuardStrategy)effector.Strategy;
                    strat.SetDamage(_attacksPlayed);
                }
            }
        }
        EffectManager.Instance.ActivateEffect(_effectsFromTrigger);
    }

    public void AddToCounter()
    {
        _attacksPlayed++;
        Debug.Log(_attacksPlayed);
    }

    public void ClearCounter()
    {
        _attacksPlayed = 0;
        foreach (CardEffect effect in _effectsFromTrigger)
        {
            foreach (CardEffector effector in effect.CardEffectors)
            {
                if (effector.Strategy is FoolsGuardStrategy)
                {
                    FoolsGuardStrategy strat = (FoolsGuardStrategy)effector.Strategy;
                    strat.SetDamage(0);
                }
            }
        }
        EffectManager.Instance.ActivateEffect(_effectsFromTrigger);
    }

}
