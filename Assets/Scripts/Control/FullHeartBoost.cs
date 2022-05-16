using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VeggieNightmare.Control
{
    public class FullHeartBoost : MonoBehaviour
    {
        [SerializeField] private float cameraViewOffset = 0.1f;
        [SerializeField] private float speed;

        private Camera mainCamera;
        private Vector3 fullHeartBoostVelocity;

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
            fullHeartBoostVelocity = Vector3.left * speed;
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsViewableInCamera()) return;
            else
            {
                Move();
            }

            DestroyIfOutOfSight();

        }

        private void Move()
        {
            transform.Translate(fullHeartBoostVelocity * Time.deltaTime);
        }

        private void DestroyIfOutOfSight()
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            if (viewportPosition.x < -0.1f) { Destroy(gameObject); }
        }

        public bool IsViewableInCamera()
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            return viewportPosition.x <= 1.0f + cameraViewOffset;
        }


    }
}
