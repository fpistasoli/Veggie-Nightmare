using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Control
{
    public class EvilTomato : EvilVeggie
    {

        [SerializeField] private float translationSpeed;
        [SerializeField] private float rotationSpeed;

        private Rigidbody rb;

        protected override void Start()
        {
            rb = GetComponent<Rigidbody>();
            base.Start();
            
        }

        protected override void Update()
        {
            base.Update();

            Debug.Log(gameObject.name + "has damage points: " + damagePoints);
            Debug.Log(gameObject.name + "has health points: " + healthPoints);
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

            MoveLeft();
            RotateLeft();
        }

        private void RotateLeft()
        {

            transform.RotateAround(GetComponent<SphereCollider>().center, Vector3.forward, rotationSpeed * Time.deltaTime);
            //transform.Rotate(new Vector3(0,0,rotationSpeed * Time.deltaTime), Space.Self);
        }

        private void MoveLeft()
        {

            //rb.AddForce(Vector3.left * translationSpeed * Time.deltaTime);
            transform.Translate(Vector3.left * translationSpeed * Time.deltaTime);

        }

        private bool IsViewableInCamera()
        {

            return true;


        }
    }
}
