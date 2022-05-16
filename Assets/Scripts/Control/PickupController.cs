using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
                onHeal?.Invoke(healthBoost); //UnityEvent
                Destroy(gameObject);
            }
        }




    }





}
