using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public virtual void OnDeSpawn()
    {
        PoolManager.Instance.DeSpawn(this);
    }
    /// <summary>
    /// called when spawned, in place of the start function
    /// </summary>
    public virtual void OnSpawn()
    {

    }
}