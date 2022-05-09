using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VeggieNightmare.Stats;

namespace VeggieNightmare.Control
{
    public abstract class EvilVeggie : MonoBehaviour
    {
        [SerializeField] protected EvilVeggieStats.Evilness evilness;
        protected float healthPoints;
        protected float damagePoints;
        protected bool isDead = false;

        //Events
        public static event Action onEvilVeggieDeath;

        protected virtual void Start()
        {
            SetHealth();
            SetDamage();
        }

        protected virtual void Update()
        {
            if(!isDead)
            {
                Move();
            }

        }

        protected abstract void SetHealth();

        protected abstract void SetDamage();


        public abstract void Move();


        public void DestroyIfOutOfSight() //se destruye si está vivo y fuera de la vista de la cámara
        {
            //TODO
        }

        public void RenderEvilnessEffectIfTough() //se ilumina de rojo si es tough
        {
            //TODO
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            if (instigator.tag == "Player")
            {
                healthPoints = Mathf.Max(0, healthPoints - damage);
            }

            if (healthPoints == 0)
            {
                onEvilVeggieDeath?.Invoke();
                isDead = true;
            }

        }

    }
}
