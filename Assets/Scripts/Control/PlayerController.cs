using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VeggieNightmare.Control
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float maxVerticalVelocityToJump = 0.5f;
        [SerializeField] private float minVerticalVelocityToJump = 0f;
        [SerializeField] private float maxVelocity;

        private bool isDead = false;

        private int jumpCount = 0;
        private int maxJumps = 2;
        private float distToGround;

        private Animator animator;
        private Rigidbody rb;

 
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();

            distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        }
        void Update()
        {
            UpdateAnimator();
            Walk();

            if(IsGrounded()) { jumpCount = 0; }

            Debug.Log(rb.velocity.y);
        }

        private void UpdateAnimator()
        {
            if(!isDead)
            {
                animator.SetBool("isWalk", true);
             
       


            }
            else
            {
                //Dead animation


            }

          
        }

        private void Walk()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        public void Jump()
        {
            if(jumpCount>=maxJumps) { return; }

            jumpCount++;
            Debug.Log(jumpCount);
            if (rb.velocity.y >= minVerticalVelocityToJump && rb.velocity.y <= maxVerticalVelocityToJump) 
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
            }
        
        }
        public void Attack()
        {
         
            //Instanciar el LaserBeam desde los ojos del player



        }

        public bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        }



    }
}
