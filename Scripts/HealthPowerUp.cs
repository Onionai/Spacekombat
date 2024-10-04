using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onion_AI
{
    public class HealthPowerUp : MonoBehaviour
    {
        //Add script to the health GameObject
        //Tag the spaceship with Player
        //This script adds 10 health to the current health in the Playercomponent, but your script are cool but confusing me lol
        //So for summary, when the player collides with the health power up, the health is increased +10 if it not full, and the power up is then destroyed
        //Test and give feedback, i hope this works

        //Onion_AI to the world


        public int healthAmount = 10f;  // Amount of health to add when picked up.

        // This method gets called when this GameObject collides with another GameObject.
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the GameObject we collided with has the tag "Player"
            if (other.CompareTag("Player"))
            {
                // Try to get the PlayerStatistic component
                PlayerStatistic playerStatistics = other.GetComponent<PlayerStatistic>();

                // If the player has a PlayerStatistic component, increase the health
                if (playerStatistics != null)
                {
                    // Ensure current health isn't already maxed out
                    if (playerStatistics.currentHealth < playerStatistics.maxHealth)
                    {
                        // Increase health by healthAmount
                        playerStatistics.currentHealth += healthAmount;

                        // Clamp the current health to not exceed max health
                        playerStatistics.currentHealth = Mathf.Clamp(playerStatistics.currentHealth, 0, playerStatistics.maxHealth);

                        // Log the health for debugging purposes
                        Debug.Log("Player Health after pickup: " + playerStatistics.currentHealth);

                        // Destroy the health pickup after use
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
