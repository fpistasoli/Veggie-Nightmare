using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
        [SerializeField] private GameObject body;
        [SerializeField] private float timeBetweenlaserAttacks;
        [SerializeField] private float blinkEffectTime = 0.1f;

        private Material[] bodyMaterials;
        private bool isDead = false;
        private float laserAttackTimer = 0f;
        private int jumpCount = 0;
        private int maxJumps = 2;
        private float distToGround;
        private Vector3 playerVelocity;
        private float fullHealth = 100f;

        private Animator animator;
        private Rigidbody rb;
        private Camera mainCamera;
        private PlayerHealth health;

        //Events
        public UnityEvent<float> onFullHealthTap;


        void Start()
        {
            mainCamera = Camera.main;

            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
            health = GetComponent<PlayerHealth>();
            distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
            bodyMaterials = body.GetComponent<SkinnedMeshRenderer>().materials;
        }

        private void OnEnable()
        {
            PlayerHealth.onDeath += OnDieHandler;
        }

        private void OnDisable()
        {
            PlayerHealth.onDeath -= OnDieHandler;
        }

        private void OnDieHandler()
        {
            isDead = true;
        }

        void Update()
        {

            if (!isDead)
            {
                UpdatePlayerVelocity();
                MovePlayer();
                MoveCamera();
                if (IsGrounded()) { jumpCount = 0; }
                ProcessInput();
            }
            
            UpdateAnimator();

        }

        private void ProcessInput()
        {

            ProcessFullHeartBoostTouch();

            //other potential touch input processes...
        }

        private void ProcessFullHeartBoostTouch()
        {
            GameObject[] fullHeartBoosts = GameObject.FindGameObjectsWithTag("FullHeartBoost");

            List<GameObject> fullHeartBoostsViewable = new List<GameObject>();

            bool canProcessInput = false;

            foreach (GameObject boost in fullHeartBoosts)
            {
                FullHeartBoost boostScript = boost.GetComponent<FullHeartBoost>();

                if(boostScript.IsViewableInCamera())
                {
                    canProcessInput = true;
                    fullHeartBoostsViewable.Add(boost);
                }
            }

            if(canProcessInput)
            {
                if (Touchscreen.current.primaryTouch.press.isPressed)
                {
                    Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                    //Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
                    Vector3 screenPoint = new Vector3(touchPosition.x, touchPosition.y, 0);

                    foreach(GameObject fullHeartBoostViewable in fullHeartBoostsViewable)
                    {
                        Ray ray = mainCamera.ScreenPointToRay(screenPoint);
                        RaycastHit[] hits = Physics.RaycastAll(ray);

                        if (hits.Length != 0)
                        {
                            for (int i = 0; i < hits.Length; i++)
                            {
                                if (hits[i].collider.tag == "FullHeartBoost")
                                {
                                    onFullHealthTap?.Invoke(fullHealth);
                                    fullHeartBoostViewable.SetActive(false);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
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


        //TODO
        public void BlinkEffect(bool isOn)
        {
            foreach (Material mat in bodyMaterials)
            {
                Color color;
                if (isOn)
                { 
                    SetAlpha(out color, mat, 0);
                }
                else
                {
                    SetAlpha(out color, mat, 1);
                }

                mat.color = color;

            }
        }

        private void SetAlpha(out Color color, Material mat, int alpha)
        {
            color = mat.color;
            color.a = alpha;
        }

        public float GetBlinkEffectTime() => blinkEffectTime;


        /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                GameObject enemy = other.gameObject;
                health.TakeDamage(enemy, enemy.GetComponent<EvilVeggie>().GetDamagePoints());

            }
        }

        private Collider GetEnemyCollider(GameObject enemy)
        {
            if (enemy.GetComponent<SphereCollider>() != null) { return enemy.GetComponent<SphereCollider>(); }
            if (enemy.GetComponent<BoxCollider>() != null) { return enemy.GetComponent<BoxCollider>(); }
            else return null;
        }

        /*
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject enemy = collision.gameObject;

                Collider enemyCollider = GetEnemyCollider(enemy);
                enemyCollider.enabled = true;
            }
        }
        */

    }
}
