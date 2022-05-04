using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VeggieNightmare.Attributes;

namespace VeggieNightmare.Control
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float maxVerticalVelocityToJump = 0.5f;
        [SerializeField] private float minVerticalVelocityToJump = 0f;
        [SerializeField] private float maxVelocity;
        [SerializeField] private GameObject laserBeamPrefab;
        [SerializeField] private GameObject laserBeamSpawnPoint;
        [SerializeField] private float timeBetweenlaserAttacks;

        private bool isDead = false;
        private float laserAttackTimer = 0f;
        private int jumpCount = 0;
        private int maxJumps = 2;
        private float distToGround;
        private Vector3 playerVelocity;

        private Animator animator;
        private Rigidbody rb;
        private Camera mainCamera;
        private PlayerHealth health;

 
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
            mainCamera = Camera.main;
            health = GetComponent<PlayerHealth>();

            distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        }
        void Update()
        {
            UpdatePlayerVelocity();
            UpdateAnimator();
            MovePlayer();
            MoveCamera();

            if(IsGrounded()) { jumpCount = 0; }

        }

        private void UpdatePlayerVelocity()
        {
            playerVelocity = Vector3.right * speed;
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

        private void MovePlayer()
        {
            transform.Translate(playerVelocity * Time.deltaTime);
        }

        private void MoveCamera()
        {
            mainCamera.transform.Translate(playerVelocity * Time.deltaTime);
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
            if(canAttack())
            {
                Instantiate(laserBeamPrefab, laserBeamSpawnPoint.transform.position, Quaternion.Euler(0, 0, 90));
                StartCoroutine(RunLaserAttackTimer());
            }

            

            //ADD TIMER: TIME BETWEEN ATTACKS (que pueda lanzar los beams cada 3 seg mínimo x ej)
        



        }

        private bool canAttack()
        {
            return laserAttackTimer == 0;
        }

        private IEnumerator RunLaserAttackTimer()
        {
            laserAttackTimer += Time.deltaTime;
            yield return new WaitForSeconds(timeBetweenlaserAttacks);
            laserAttackTimer = 0;
        }

        public bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        }

    }
}
