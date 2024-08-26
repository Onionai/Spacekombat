using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onion_AI
{
    public class Boundaries : MonoBehaviour
    {
        GameManager gameManager;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            IReleaseFromPool poolReleaser = other.GetComponent<IReleaseFromPool>();
            if(poolReleaser != null)
            {
                poolReleaser.ReleaseFromPool();
            }
        }

        //Functionalities

        public void Initialize(GameManager gM)
        {
            gameManager = gM;
        }
    }
}
