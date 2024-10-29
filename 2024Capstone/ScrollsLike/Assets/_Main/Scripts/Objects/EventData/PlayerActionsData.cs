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

    
    #endregion

    #region EventMethods
    public void HandlePlayerMovement(Vector2 movement) => PlayerMoveEvent.Invoke(movement);
    public void HandlePlayerLook(Vector2 look) => PlayerLookEvent.Invoke(look);

    #endregion
}
