using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceZone : Singleton<StanceZone>
{
    private GameCard _stance;

    public void AddStance(StanceData data)
    {
        if(_stance != null)
        {
            _stance.OnDeSpawn();
        }
        _stance = PoolManager.Instance.Spawn("Card").GetComponent<GameCard>();
        data.Activate();
        _stance.ReferenceCardData = data;
        _stance.transform.SetParent(transform);
        _stance.transform.position = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
