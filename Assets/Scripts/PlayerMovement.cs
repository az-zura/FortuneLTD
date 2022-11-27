using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     public CharacterController controller;
        [SerializeField] private float speed = 4f; 
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private float directionInterpolation;

        private Vector3 direction = new Vector3();
        
        
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
}
