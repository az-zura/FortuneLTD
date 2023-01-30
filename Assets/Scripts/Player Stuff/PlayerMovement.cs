using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private InputManager _inputManager;
    private MainMenuInputManager _mainMenuInputManager;
    
    private Vector3 moveDirection;
    [SerializeField] private float directionInterpolation;
    private Vector3 direction = new Vector3();
    
    private CharacterController controller;
    private bool isMoving;
    
    [SerializeField] private float speed = 4f;
    [SerializeField] private float roadSpeed = 8f;
    [SerializeField] private float acceleration = 1f;

    [SerializeField] private bool isMainMenu;
    public event EventHandler UpdateMoving;
    private GhostAnimation _ghostAnimation;
    private float currentSpeed;
    private float currentAcceleration;
    
    private int onRoadTriggerBoxCount;

    private void Awake()
    {
        if (isMainMenu)
        {
            _mainMenuInputManager = GetComponent<MainMenuInputManager>();
        }
        else
        {
            _inputManager = GetComponent<InputManager>();
        }
        controller = GetComponent<CharacterController>();
        _ghostAnimation = GetComponentInChildren<GhostAnimation>();

        currentSpeed = speed;
    }

    public void HandleAllMovement() 
    {
        HandleMovement();
    }

    private void Update()
    {
        var newSpeed = currentSpeed + currentAcceleration * Time.deltaTime;
        if (newSpeed >= speed && newSpeed <= roadSpeed)
        {
            currentSpeed = newSpeed;
        }
    }

    private void HandleMovement()
    {
        bool currentMovingState = isMoving;
        Vector3 normalizedDirection;
        if (isMainMenu)
        {
            normalizedDirection = new Vector3(_mainMenuInputManager.horizontalInput, 0, _mainMenuInputManager.verticalInput).normalized;
        }
        else
        {
            normalizedDirection = new Vector3(_inputManager.horizontalInput, 0, _inputManager.verticalInput).normalized;
        }
        direction = Vector3.Lerp(direction,  normalizedDirection , directionInterpolation).normalized;
        if (normalizedDirection.magnitude >= 0.1f)
        {
            if (!isMoving)
            {
                AudioManager.instance.PlaySound("Ghost", gameObject);
            }
            controller.Move(direction * (currentSpeed * Time.deltaTime));
            isMoving = true;
        }
        else
        {
            if (isMoving)
            {
                AudioManager.instance.StopSound("Ghost", gameObject);
            }
            isMoving = false;
        }

        if (isMoving != currentMovingState)
        {
            _ghostAnimation.setMoving(isMoving);
        }
    }

    public void IncrementRoadTriggerCount()
    {
        onRoadTriggerBoxCount++;
        if (onRoadTriggerBoxCount > 0)
        {
            currentAcceleration = acceleration;
        }
    }    
    public void DecrementRoadTriggerCount()
    {
        onRoadTriggerBoxCount--;

        if (onRoadTriggerBoxCount <= 0)
        {
            currentAcceleration = -acceleration;
        }
    }
    
    
    




    /*

        [SerializeField] private CapsuleCollider _collider;

        
        
        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            
            
            direction = Vector3.Lerp(direction,new Vector3(horizontal, 0, vertical).normalized,directionInterpolation);
       
            if (direction.magnitude >= 0.1f)
            { 
                controller.Move(direction.normalized * (speed * Time.deltaTime));
            }
       
            if (Input.GetKey(KeyCode.Space))
            {
                controller.Move(Vector3.up * (speed * Time.deltaTime));
            }
               
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(Vector3.down * (speed * Time.deltaTime));
            }
    
        }
        */
}
