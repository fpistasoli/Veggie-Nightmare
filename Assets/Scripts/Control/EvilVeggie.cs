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
        [SerializeField] private GameObject toughEffect;
        [SerializeField] private GameObject body;
        [SerializeField] private float blinkEffectTime = 0.5f;

        protected float healthPoints;
        protected float damagePoints;
        protected bool isDead = false;
        protected Camera mainCamera;

        //Events
        public static event Action onEvilVeggieDeath;

        protected virtual void Start()
        {
            mainCamera = Camera.main;

            SetHealth();
            SetDamage();
            SetEvilnessEffect();
        }

        private void SetEvilnessEffect()
        {
            if (evilness == EvilVeggieStats.Evilness.Mild) { toughEffect.SetActive(false); }
        }

        protected virtual void Update()
        {
            if(!isDead)
            {
                Move();
                DestroyIfOutOfSight();
            }

            //Debug.Log("EVIL " + gameObject.name + " HAS " + healthPoints + " HEALTH POINTS");
        }

        protected abstract void SetHealth();

        protected abstract void SetDamage();


        public abstract void Move();


        public void DestroyIfOutOfSight()
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            if (viewportPosition.x < -0.1f) { Destroy(gameObject); }
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


                //TODO: Run explosion animation


            }

        }

        public void BlinkEffect()
        {
            Color oldMaterialColor = body.GetComponent<MeshRenderer>().materials[0].color;
            body.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", Color.white);
            StartCoroutine(BlinkCoroutine(oldMaterialColor)); 
        }

        private IEnumerator BlinkCoroutine(Color oldMaterialColor)
        {
            yield return new WaitForSeconds(blinkEffectTime);
            body.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", oldMaterialColor);
        }

        public float GetDamagePoints() => damagePoints;


    }
}
