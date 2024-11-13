using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameEffect : PoolObject
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        GetComponent<Canvas>().overrideSorting = true;
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        animator.Play("Effect");
        CardGameManager.Instance.Wait();
    }

    public void Finished()
    {
        CardGameManager.Instance.EffectDone();
        OnDeSpawn();
    }
}
