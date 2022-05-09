using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Control
{
    public class EvilCarrot : EvilVeggie
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private float distToGroundOffset = 0.6f;
        [SerializeField] private float maxVelocity;

        private float distToGround;
        private Rigidbody rb;

        protected override void Start()
        {
            rb = GetComponent<Rigidbody>();
            distToGround = GetComponent<BoxCollider>().bounds.extents.y;
            base.Start();
        }

        private void FixedUpdate()
        {
            if (IsGrounded())
            {
                rb.velocity = Vector3.zero;
                Jump();
            }
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void SetHealth()
        {
            float hp = EvilVeggieStats.sharedInstance.GetHealthPointsMild()[2];

            if (evilness == EvilVeggieStats.Evilness.Mild)
            {
                healthPoints = hp;
            }
            else if (evilness == EvilVeggieStats.Evilness.Tough)
            {
                healthPoints = hp * 2;
            }

        }

        protected override void SetDamage()
        {
            float damageP = EvilVeggieStats.sharedInstance.GetDamagePointsMild()[2];

            if (evilness == EvilVeggieStats.Evilness.Mild)
            {
                damagePoints = damageP;
            }
            else if (evilness == EvilVeggieStats.Evilness.Tough)
            {
                damagePoints = damageP * 2;
            }


        }

        public override void Move()
        {
            //if(IsGrounded())
           // {
           //     rb.velocity = Vector3.zero;
           //     Jump();
           // }
        }

        private void Jump()
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }

        public bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + distToGroundOffset);
        }


    }

}