using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onion_AI
{

    //This script suppose work for any simple project like this
    //Scroll down, you can see where to add the functions for deflecting bullet or shit like that

    public class ShieldPowerUp : MonoBehaviour
    {
        public GameObject shieldPrefab;  // Reference to the shield prefab to enable/disable
        private GameObject shieldInstance;
        private bool shieldActive = false;

        // Duration for the shield to stay active
        public float shieldDuration = 5.0f;


        // Detect collision with the player
        void OnTriggerEnter(Collider other)
        {
            // Check if the object colliding is tagged "Player"
            if (other.gameObject.CompareTag("Player"))
            {
                if (!shieldActive)
                {
                    StartCoroutine(ActivateShield(other.gameObject));
                }
            }
        }

        // Coroutine to enable shield and disable after 5 seconds
        IEnumerator ActivateShield(GameObject player)
        {
            shieldActive = true;

            // Enable the shield on the player
            shieldInstance = Instantiate(shieldPrefab, player.transform);
            shieldInstance.transform.localPosition = Vector3.zero; // Position it around the player

            // Shield stays active for 5 seconds
            yield return new WaitForSeconds(shieldDuration);

            // Disable the shield
            Destroy(shieldInstance);
            shieldActive = false;

            // Optionally, you can destroy the power-up object after using it
            Destroy(gameObject);
        }

        // Handle bullet collision (this will be implemented in another script)
        void OnCollisionEnter(Collision collision)
        {
            // Check if the object is tagged as "Bullet"
            if (collision.gameObject.CompareTag("Bullet"))
            {
                // You can add logic here to destroy bullets or deflect them, e.g.
                // Destroy(collision.gameObject); // Example of destroying bullets on collision with the shield
                //You understand

                //HERE HERE
                //Add your functions here
            }
        }
    }
}