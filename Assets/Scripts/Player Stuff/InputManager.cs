using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    private Camera deskCamera;
    
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
    private void Start()
    {
        _playerInput.Player.Click.started += _ => StartedClick();
        _playerInput.Player.Click.performed += _ => EndedClick();
    }

    private void StartedClick()
    {
        
    }
    private void EndedClick()
    {
        
    }

    private void DetectObject()
    {
        
    }
}
