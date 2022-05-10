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
            }
        }

    }

}