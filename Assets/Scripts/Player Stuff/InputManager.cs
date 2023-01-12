using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    [SerializeField] private Camera deskCamera;
    private DeskManager _deskManager;


    public PlayerInput GetPlayerInput()
    {
        return _playerInput;
    }

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
        _playerInput.Player.Click.started += _ => ClickStarted();
        _playerInput.Player.Click.performed += _ => ClickPerformed();
        _deskManager = GameObject.Find("Desk").GetComponent<DeskManager>();
        Debug.Log(_deskManager.name);
    }
    
    private void ClickStarted()
    {
    }

    private void ClickPerformed()
    {
        DetectObject();
    }

    private void DetectObject()
    {
        Ray ray = deskCamera.ScreenPointToRay(_playerInput.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Joe Desk")
                {
                    _deskManager.ObjectHit(hit.collider.name);
                    Debug.Log("HIT RIGHT OBJECT");
                }
                //Debug.Log("3D hit: " + hit.collider.name);
            }
        }
    }
    
}
