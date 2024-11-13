using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameEffect : PoolObject
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.Play("Effect");
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        animator.Play("Effect");
    }

    public void Finished()
    {
        OnDeSpawn();
    }
}
