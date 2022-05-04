using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VeggieNightmare.Attributes
{
    public class PlayerHealth : MonoBehaviour
    {

        private float healthPoints; // [ 0% , 100% ]
        bool isDead = false;

        public static event Action onDeath;


        // Start is called before the first frame update
        void Start()
        {
            healthPoints = 100;

        }

        // Update is called once per frame
        void Update()
        {

            CheckIfDead();

        }

        private void CheckIfDead()
        {
            if (healthPoints == 0)
            {
                //onDeath?.Invoke();
                //TODO: agregar subscribers en las clases que se suscriben a este evento
                isDead = true;
            }

        }

        public void TakeDamage(GameObject instigator, float damage)
        {

            if(instigator.tag == "Enemy")
            {
                healthPoints = Mathf.Max(0, healthPoints - damage);
            }

                
        }





    }

}
