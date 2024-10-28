using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    //will have a list of effects to que through each phase in order to better handle time slots
    private void Start()
    {
        
    }

    public void ActivateEffect(List<CardEffect> effects, TimeSlot slotReference = null)
    {
        StartCoroutine(PlayEffect(effects, slotReference));
    }

    //activates all qued effects
    IEnumerator PlayEffect(List<CardEffect> effects, TimeSlot slotReference)
    {
        foreach(CardEffect effect in effects)
        {
            effect.Effect();
            yield return new WaitForSeconds(1);
        }

        slotReference.CardResolved();
        yield return null;
    }
}
