using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Control
{
    public class EvilTomato : EvilVeggie, IRollable
    {

        [SerializeField] private float translationSpeed = 1.0f;
        [SerializeField] private float maxSpeed = 2.5f;
        [SerializeField] private float cameraViewOffset = 0.1f;

        private float radius;
        private Rigidbody rb;

        protected override void Start()
        {
            rb = GetComponent<Rigidbody>();
            radius = GetComponent<SphereCollider>().radius;
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        private void FixedUpdate()
        {
            if (!IsViewableInCamera()) return;
            else
            {
                MoveLeft();
            }
        }

        private void LateUpdate()
        {
            if (!IsViewableInCamera()) return;
            else
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }

        protected override void SetHealth()
        {
            float hp = EvilVeggieStats.sharedInstance.GetHealthPointsMild()[1];

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
            float damageP = EvilVeggieStats.sharedInstance.GetDamagePointsMild()[1];

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
            if (!IsViewableInCamera()) return;
            else
            {
                Roll();
            }

        }

        public void Roll()
        {
            //Distance travelled
            Vector3 movement = rb.velocity * Time.deltaTime;   //distance travelled in delta time seconds (movimiento circular uniforme)
            float distance = movement.magnitude;

            //Rotation
            float angle = distance * (180f / Mathf.PI) / radius; //angle formula (movimiento circular uniforme)
            transform.localRotation = Quaternion.Euler(Vector3.forward * angle) * transform.localRotation;
        }

        private void MoveLeft()
        {
            rb.AddForce(Vector3.left * translationSpeed * Time.deltaTime, ForceMode.Impulse);
        }

        private bool IsViewableInCamera()
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            return viewportPosition.x <= 1.0f + cameraViewOffset;
        }
    }
}
