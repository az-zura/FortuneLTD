using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputManager _inputManager;
    
    private Vector3 moveDirection;
    [SerializeField] private float directionInterpolation;
    private Vector3 direction = new Vector3();
    
    private CharacterController controller;
    
    [SerializeField] private float speed = 4f;


    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        controller = GetComponent<CharacterController>();
    }

    public void HandleAllMovement() //also floating but not implemented yet
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var normalizedDirection = new Vector3(_inputManager.horizontalInput, 0, _inputManager.verticalInput).normalized;
        direction = Vector3.Lerp(direction,  normalizedDirection , directionInterpolation).normalized;
        if (normalizedDirection.magnitude >= 0.1f)
        {
            controller.Move(direction * (speed * Time.deltaTime));
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
