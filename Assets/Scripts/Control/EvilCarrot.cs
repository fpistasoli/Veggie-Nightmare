using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Control
{
    public class EvilCarrot : EvilVeggie
    {
        protected override void Start()
        {
            base.Start();

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

        }





    }

}