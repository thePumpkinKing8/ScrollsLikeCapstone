using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    //will have a list of effects to que through each phase in order to better handle time slots
    private void Start()
    {
        
    }

    public void ActivateEffect(List<CardEffect> effects, TimeSlot target = null)
    {
        StartCoroutine(PlayEffect(effects, target));
    }

    //activates all qued effects
    IEnumerator PlayEffect(List<CardEffect> effects, TimeSlot target)
    {
        foreach(CardEffect effect in effects)
        {
            effect.Effect(target);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => CardGameManager.Instance.CurrentPhase != Phase.EffectMode);
        }
        //tell the card game manager that it can continue to allow effects to be played
       // CardGameManager.Instance.EffectDone();
        yield return null;
    }
}
