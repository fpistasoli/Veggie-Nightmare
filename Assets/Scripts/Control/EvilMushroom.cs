using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Control
{
    public class EvilMushroom : EvilVeggie, IRollable
    {

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
        }

        protected override void SetHealth()
        {
            float hp = EvilVeggieStats.sharedInstance.GetHealthPointsMild()[0];

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
            float damageP = EvilVeggieStats.sharedInstance.GetDamagePointsMild()[0];

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

            //Roll();
        }

        public void Roll()
        {
            Quaternion currentRotation = transform.rotation;

            Quaternion targetRotation = Quaternion.Euler(0, 180, 0);
            
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
 



    }
}
