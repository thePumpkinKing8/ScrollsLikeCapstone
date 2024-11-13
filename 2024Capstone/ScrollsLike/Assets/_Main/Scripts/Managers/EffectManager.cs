using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    private Queue<Tuple<List<CardEffect>, TimeSlot>> _effectQue = new Queue<Tuple<List<CardEffect>, TimeSlot>>();
    private bool _effectPlaying = false;
    private void Start()
    {
        
    }

    public void ActivateEffect(List<CardEffect> effects, TimeSlot target = null)
    {
        if(_effectPlaying)
        {
            //_effectQue.Enqueue(new Tuple<List<CardEffect>, TimeSlot>(effects,target));
        }
        else
            StartCoroutine(PlayEffect(effects, target));
    }

    //activates all qued effects
    IEnumerator PlayEffect(List<CardEffect> effects, TimeSlot target)
    {
        bool trigger = false;
        Action action = () => trigger = true;
        CardGameManager.Instance.Events.EffectEnded.AddListener(action.Invoke);
        foreach(CardEffect effect in effects)
        {
            effect.Effect(target);
            yield return new WaitUntil(() => trigger == true);
            trigger = false;
        }
        _effectQue.Dequeue();
        NextInQue();
        //tell the card game manager that it can continue to allow effects to be played
       // CardGameManager.Instance.EffectDone();
        yield return null;
    }

    private void NextInQue()
    {
        if(_effectQue.Count > 0)
        {
            ///StartCoroutine(PlayEffect(_effectQue[0].Item1, _effectQue[0].Item2));
        }
        else
        {
            CardGameManager.Instance.EffectPermission();
        }
    }

    public bool GetPermission()
    {
        if (_effectQue.Count > 0)
        {
            return false;
        }
        else 
            return true;
    }
}
