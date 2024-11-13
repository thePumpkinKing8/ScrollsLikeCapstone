using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceZone : Singleton<StanceZone>
{
    private GameCard _stance;
    private StanceData _stanceData;

    public void AddStance(StanceData data)
    {
        if(_stance != null)
        {
            _stanceData.Deactivate();
            _stance.OnDeSpawn();           
        }
        _stance = PoolManager.Instance.Spawn("Card").GetComponent<GameCard>();
        _stanceData = data;
        _stanceData.Activate();
        _stance.ReferenceCardData = data;
        _stance.transform.SetParent(transform);
        _stance.transform.position = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
