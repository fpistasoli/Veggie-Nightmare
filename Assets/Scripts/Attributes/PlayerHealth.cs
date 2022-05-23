using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Control;

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
            Debug.Log("HEALTH POINTS: " + healthPoints + "/100");

        }


        public void TakeDamage(GameObject instigator, float damage)
        {
            if(instigator.tag == "Enemy")
            {
                healthPoints = Mathf.Max(0, healthPoints - damage);
            }

            if (healthPoints == 0)
            {
                onDeath?.Invoke();
                isDead = true;
            }
        }

        public void Heal(float healthBoost)
        {
            healthPoints = Mathf.Min(healthPoints + healthBoost, 100);
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }



    }

}
