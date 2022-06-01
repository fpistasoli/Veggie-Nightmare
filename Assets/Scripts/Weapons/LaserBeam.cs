using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Control;

namespace VeggieNightmare.Weapons
{
    public class LaserBeam : MonoBehaviour
    {

        [SerializeField] private float speed = 1f;
        [SerializeField] private float damage = 1f;

        private Camera mainCamera;
        private GameObject player;


        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
            player = GameObject.FindWithTag("Player");
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
            if (viewportPosition.x > 1) Destroy(gameObject);
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

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                GameObject enemy = collision.gameObject;
                enemy.GetComponent<EvilVeggie>().TakeDamage(player, damage);
                enemy.GetComponent<EvilVeggie>().BlinkEffect();
                Destroy(gameObject);
            }
            



        }



    }

}