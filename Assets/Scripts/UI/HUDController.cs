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
            PlayerHealth.onDeath += OnGameOverUIHandler;
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

        private void OnDestroy()
        {
            PlayerHealth.onDeath -= OnGameOverUIHandler;
        }


    }

}