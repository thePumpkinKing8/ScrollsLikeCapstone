using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private ActionAsset _input;
    public ActionAsset Input { get { return _input; } }
    [SerializeField] private PlayerActionsData _actions;
    public PlayerActionsData ActionsData { get { return _actions; } private set { _actions = value; } }


    private void OnEnable()
    {
        if((Input == null))
        {
            _input = new ActionAsset();
            _input.Gameplay.Movement.performed += (val) => _actions.HandlePlayerMovement(val.ReadValue<Vector2>());
            _input.Gameplay.Look.performed += (val) => _actions.HandlePlayerLook(val.ReadValue<Vector2>());

        }
        _input.Enable();
    }
}
