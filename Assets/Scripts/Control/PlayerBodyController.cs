using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Attributes;

namespace VeggieNightmare.Control
{
    public class PlayerBodyController : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                GameObject enemy = other.gameObject;
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(enemy, enemy.GetComponent<EvilVeggie>().GetDamagePoints());

                StartCoroutine(BlinkEffectCoroutine(player));
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