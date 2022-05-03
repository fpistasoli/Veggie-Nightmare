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
            //avanzar con translate/addForce



            //si el laserbeam esta fuera de los limites de la follow camera, destruirlo

        }
    }

}