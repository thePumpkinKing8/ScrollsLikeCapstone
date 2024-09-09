using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerActionsData", menuName = "SOs/PlayerActionsData", order = 0)]
public class PlayerActionsData : ScriptableObject
{
    #region EventSetUp
    public UnityEvent<Vector2> PlayerMoveEvent;
    public UnityEvent<Vector2> PlayerLookEvent;

    public UnityEvent PlayerStartAttackEvent;
    public UnityEvent PlayerEndAttackEvent;

    public UnityEvent PLayerStartBlockEvent;
    public UnityEvent PLayerEndBlockEvent;
    #endregion

    #region EventMethods
    public void HandlePlayerMovement(Vector2 movement) => PlayerMoveEvent.Invoke(movement);
    public void HandlePlayerLook(Vector2 look) => PlayerLookEvent.Invoke(look);

    public void HandlePlayerAttack() => PlayerStartAttackEvent.Invoke();
    public void HandlePlayerEndAttack() => PlayerEndAttackEvent.Invoke();
    public void HandlePlayerBlock() => PLayerStartBlockEvent.Invoke();
    public void HandlePlayerEndBlock() => PLayerEndBlockEvent.Invoke();
    #endregion
}
