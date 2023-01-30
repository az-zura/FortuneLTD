using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private MainMenuInputManager _mainMenuInputManager;
    private PlayerMovement _playerMovement;
    [SerializeField] private bool isMainLevel;
    private void Awake()
    {
        if (isMainLevel)
        {
            _mainMenuInputManager = GetComponent<MainMenuInputManager>();
        }
        else
        {
            _inputManager = GetComponent<InputManager>();
        }
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (isMainLevel)
        {
            _mainMenuInputManager.HandleAllInputs();
        }
        else
        {
            _inputManager.HandleAllInputs();
        }
    }

    private void FixedUpdate()
    {
        _playerMovement.HandleAllMovement();
    }
}
