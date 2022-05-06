using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VeggieNightmare.Attributes;
using VeggieNightmare.Control;

namespace VeggieNightmare.UI
{
    public class HUDController : MonoBehaviour
    {

        [SerializeField] private Button jumpButton;
        [SerializeField] private GameObject player;

        private void Start()
        {
          
        }

        private void OnEnable() //aca van las suscripciones a eventos
        {
            PlayerHealth.onDeath += OnGameOverUIHandler;
        }

        private void OnDisable() //aca van las desuscripciones a eventos
        {
            PlayerHealth.onDeath -= OnGameOverUIHandler;
        }

        private void OnGameOverUIHandler()
        {
            
            //TODO

        }

        public void Jump()
        {
            player.GetComponent<PlayerController>().Jump();
        }

        public void Attack()
        {
            player.GetComponent<PlayerController>().Attack();
        }



    }

}