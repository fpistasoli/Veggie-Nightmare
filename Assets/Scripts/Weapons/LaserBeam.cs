using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VeggieNightmare.Weapons
{
    public class LaserBeam : MonoBehaviour
    {

        [SerializeField] private float speed = 1f; 

        private Camera mainCamera;



        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;

        }

        // Update is called once per frame
        void Update()
        {
            Move();
            DestroyIfOutOfSight();
            
        }

        private void DestroyIfOutOfSight()
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            if (viewportPosition.x > 1) { Destroy(gameObject); }
        }

        private void Move()
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);

            if(OutOfCameraBounds())
            {
                Destroy(gameObject);
            }

        }

        private bool OutOfCameraBounds()
        {
            //TODO
            return false;
           



        }
    }

}