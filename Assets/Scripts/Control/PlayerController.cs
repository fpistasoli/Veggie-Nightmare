using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VeggieNightmare.Control
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float speed = 1f;

 
        void Start()
        {
           
            
        }
        void Update()
        {
            Walk();

        }

        private void Walk()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        private void FixedUpdate()
        {








        }


    }
}
