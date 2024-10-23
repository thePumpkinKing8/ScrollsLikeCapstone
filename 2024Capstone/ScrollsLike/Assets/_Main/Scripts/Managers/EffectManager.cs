using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    //will have a list of effects to que through each phase in order to better handle time slots
    private void Start()
    {
        CardGameManager.Instance.Events.EffectPlayed.AddListener(ActivateEffect);
    }

    public void ActivateEffect(List<CardEffect> effects)
    {
        StartCoroutine(PlayEffect(effects));
    }

    //activates all qued effects
    IEnumerator PlayEffect(List<CardEffect> effects)
    {
        foreach(CardEffect effect in effects)
        {
            effect.Effect();
            yield return new WaitForSeconds(1);
        }

        CardGameManager.Instance.EffectDone();
        yield return null;
    }
}
