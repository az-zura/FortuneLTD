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
    [SerializeField] private Camera folderCamera;
    [SerializeField] private Camera monitorCamera;
    [SerializeField] private Camera rulesheetCamera;
    [SerializeField] private GameObject deskGameobject;

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
        _deskManager = deskGameobject.GetComponent<DeskManager>();
    }

    private void ClickStarted()
    {
    }

    private void ClickPerformed()
    {
        if (!DetectObject())
        {
            CloseCurrentAction();
        }
    }

    private void CloseCurrentAction()
    {
        _deskManager.CloseCurrentAction();
    }

    private bool DetectObject()
    {
        Ray ray = GetActiveCamera().ScreenPointToRay(_playerInput.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Joe Desk")
                {
                    _deskManager.ObjectHit(hit.collider.name);
                    return true;
                }
            }
        }

        return false;
    }

    private Camera GetActiveCamera()
    {
        switch (_deskManager.GetCurrentState())
        {
            case 0:
                return deskCamera;
                break;
            case 1:
                return folderCamera;
                break;
        }
        return Camera.current;
    }
}
