using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameEffect : PoolObject
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSO _sound;


    private void Start()
    {
        GetComponent<Canvas>().overrideSorting = true;
    }
    public override void OnSpawn()
    {
        Debug.Log("spawned");
        base.OnSpawn();
        BlakesAudioManager.Instance.PlayAudio(_sound.AudioName);
        _animator.Play("Effect");
       // CardGameManager.Instance.WaitForEffects();
    }

    public void Finished()
    {
        CardGameManager.Instance.EffectDone();
        OnDeSpawn();
    }
}
