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
            _effectQue.Enqueue(new Tuple<List<CardEffect>, TimeSlot>(effects,target));
        }
        else
        {
            _effectQue.Enqueue(new Tuple<List<CardEffect>, TimeSlot>(effects, target));
            StartCoroutine(PlayEffect(effects, target));
        }
            
    }

    //activates all qued effects
    IEnumerator PlayEffect(List<CardEffect> effects, TimeSlot target)
    {
        _effectPlaying = true;
        bool trigger = false;
        Action action = () => trigger = true;
        CardGameManager.Instance.Events.EffectEnded.AddListener(action.Invoke);
        foreach(CardEffect effect in effects)
        {
            effect.Effect(HealthManager.Instance,target);
            yield return new WaitUntil(() => trigger == true);
            trigger = false;
        }
        _effectQue.Dequeue();
        NextInQue();

        yield return null;
    }

    private void NextInQue()
    {
        if(_effectQue.Count > 0)
        {
            StartCoroutine(PlayEffect(_effectQue.Peek().Item1, _effectQue.Peek().Item2));
        }
        else
        {
            _effectPlaying = false;
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
