using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VeggieNightmare.Stats;
using VeggieNightmare.Core;

namespace VeggieNightmare.Control
{
    public abstract class EvilVeggie : MonoBehaviour
    {
        [SerializeField] protected EvilVeggieStats.Evilness evilness;
        [SerializeField] private GameObject toughEffect;
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private GameObject body;
        [SerializeField] private float blinkEffectTime = 0.5f;
        [SerializeField] private float deathEffectDuration = 1.0f;

        protected float healthPoints;
        protected float damagePoints;
        protected bool isDead = false;
        protected Camera mainCamera;
    

        //Events
        public static event Action onEvilVeggieDamageTaken;

        protected virtual void Start()
        {
            mainCamera = Camera.main;

            SetHealth();
            SetDamage();
            SetEvilnessEffect();
            HideDeathEffect();
        }

        private void HideDeathEffect()
        {
            deathEffect.SetActive(false);
        }

        private void SetEvilnessEffect()
        {
            if (evilness == EvilVeggieStats.Evilness.Mild) { toughEffect.SetActive(false); }
        }

        protected virtual void Update()
        {
            Move();
            DestroyIfOutOfSight();
        }

        protected abstract void SetHealth();

        protected abstract void SetDamage();


        public abstract void Move();


        public void DestroyIfOutOfSight()
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            if (viewportPosition.x < -0.1f || viewportPosition.y < -0.1f) { Destroy(gameObject); }
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            if (instigator.tag == "Player")
            {
                AudioSource src = mainCamera.GetComponent<AudioSource>();
                src.PlayOneShot(AudioManager.sharedInstance.GetClip(6));

                healthPoints = Mathf.Max(0, healthPoints - damage);
                onEvilVeggieDamageTaken?.Invoke();
            }

            if (healthPoints == 0)
            {
                AudioSource src = mainCamera.GetComponent<AudioSource>();
                src.PlayOneShot(AudioManager.sharedInstance.GetClip(7));

                isDead = true;

                GetComponent<Collider>().enabled = false;

                PlayDeathAnimation();
            }

        }

        private void PlayDeathAnimation()
        {
            
            PlayDeathEffect();
            HideEvilVeggie();
        }

        private void PlayDeathEffect()
        {
            Transform particleSystemTransform = deathEffect.transform.GetChild(0);
            ParticleSystem particleSystem = particleSystemTransform.GetComponent<ParticleSystem>();

            if (!particleSystem.isPlaying) 
            {
                particleSystem.Play();
                StartCoroutine(WaitForOneParticleEffectCycle(particleSystem));
            }
        }

        private IEnumerator WaitForOneParticleEffectCycle(ParticleSystem particleSystem)
        {
            yield return new WaitForSeconds(deathEffectDuration);
            particleSystem.gameObject.SetActive(false);
        }

        private void HideEvilVeggie()
        {
            foreach(Transform child in transform)
            {
                if(child.tag != "DeathEffect")
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
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
