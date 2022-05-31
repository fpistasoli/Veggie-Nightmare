using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VeggieNightmare.Attributes;
using VeggieNightmare.UI;

namespace VeggieNightmare.Control
{
    public class PickupController : MonoBehaviour
    {

        [SerializeField] private float healthBoost = 5.0f;

        private Camera mainCamera;

        //Events
        public UnityEvent<float> onHeal;


        void Start()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            if (viewportPosition.x < -0.1f) { Destroy(gameObject); }
        }



        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerHealth>().Heal(healthBoost);
                onHeal?.Invoke(healthBoost); //UnityEvent
                GameObject hudGO = GameObject.Find("HUD"); //lo hago asi porque no me lo toma si lo hago con unity event
                hudGO.GetComponent<HUDController>().OnHPUpdateUI(); 
                Destroy(gameObject);
            } 
        }



            
    }





}
