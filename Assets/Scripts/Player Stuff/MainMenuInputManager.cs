using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
  

    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        _playerInput.Enable();

    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    //clicking on 3d Objects with mouse left click
    

    private void EscapePressed()
    {
        //close game
    }

    
    
}
