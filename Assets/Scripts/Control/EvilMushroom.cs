using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Control
{
    public class EvilMushroom : EvilVeggie
    {

        protected override void Start()
        {
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
            //static enemy
        }





    }
}
