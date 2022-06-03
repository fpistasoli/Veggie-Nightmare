using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VeggieNightmare.Attributes;
using VeggieNightmare.Core;

namespace VeggieNightmare.Control
{
    public class PlayerBodyController : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        public UnityEvent<float> onEnemyAttack;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {

                AudioSource src = Camera.main.GetComponent<AudioSource>();
                src.PlayOneShot(AudioManager.sharedInstance.GetClip(14));

                GameObject enemy = other.gameObject;
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(enemy, enemy.GetComponent<EvilVeggie>().GetDamagePoints());

                onEnemyAttack?.Invoke(playerHealth.GetHealthPoints());

                if(!player.GetComponent<PlayerController>().IsDead())
                {
                    StartCoroutine(BlinkEffectCoroutine(player));
                }
                

            }
        }

        private IEnumerator BlinkEffectCoroutine(GameObject player)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            float blinkEffectTime = playerController.GetBlinkEffectTime();

            playerController.BlinkEffect(true);
            yield return new WaitForSeconds(blinkEffectTime);
            playerController.BlinkEffect(false);
            yield return new WaitForSeconds(blinkEffectTime);
            playerController.BlinkEffect(true);
            yield return new WaitForSeconds(blinkEffectTime);
            playerController.BlinkEffect(false);
        }

        /* 
         private void OnTriggerExit(Collider other)
         {
             if (other.gameObject.tag == "Enemy")
             {
                 player.GetComponent<PlayerController>().BlinkEffect(false);
             }
         }
        */



    }

}