using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VeggieNightmare.Control;

namespace VeggieNightmare.UI
{
    public class HUDController : MonoBehaviour
    {

        [SerializeField] private Button jumpButton;
        [SerializeField] private GameObject player;

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