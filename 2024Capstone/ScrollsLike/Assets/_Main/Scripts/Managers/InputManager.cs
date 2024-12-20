using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private ActionAsset _input;
    public ActionAsset Input { get { return _input; } }
    [SerializeField] private PlayerActionsData _actions;
    public PlayerActionsData ActionsData { get { return _actions; } private set { _actions = value; } }

    public bool IsPaused { get; private set; } = false;
    public bool IsDead { get; private set; } = false;

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new ActionAsset();
            _input.Gameplay.Movement.performed += (val) => _actions.HandlePlayerMovement(val.ReadValue<Vector2>());
            _input.Gameplay.Look.performed += (val) => _actions.HandlePlayerLook(val.ReadValue<Vector2>());

            _input.UI.PauseGame.performed += TogglePause;
            _input.UI.OpenInventory.performed += ToggleInventory;
        }
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.UI.PauseGame.performed -= TogglePause;
        _input.UI.OpenInventory.performed -= ToggleInventory;
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if (DeckInventoryUI.Instance != null && GameManager.Instance.State != GameState.CardGame && !IsPaused)
        {
            DeckInventoryUI.Instance.ToggleInventory();
        }
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (PauseMenu.Instance != null)
        {           
            IsPaused = !IsPaused;
            PauseMenu.Instance.TogglePause();

            if (IsPaused)
            {
                _input.Gameplay.Disable(); 
            }
            else
            {
                _input.Gameplay.Enable();
            }

            Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = IsPaused;
        }
    }

    public void EnableGameplay()
    {
        _input.Gameplay.Enable();
        IsPaused = false;
        Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = IsPaused;
    }
}
