using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VeggieNightmare.Control;

namespace VeggieNightmare.SceneManagement
{
    public class MirrorExit : MonoBehaviour
    {

        bool hasReachedExit = false;
        public static event Action onStageComplete;

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                if (!hasReachedExit && other.GetComponent<PlayerController>().IsGrounded())
                {
                    hasReachedExit = true;

                    GameObject player = other.gameObject;
                    player.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0)) * player.transform.localRotation;

                    StartCoroutine(StageCompleteCoroutine());
                }
            }
        }

        private IEnumerator StageCompleteCoroutine()
        {
            yield return new WaitForSeconds(1f);

            onStageComplete?.Invoke();
        }
    }

}
