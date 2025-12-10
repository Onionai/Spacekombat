using UnityEngine;

namespace Onion_AI
{
    public class PowerUpClass : MonoBehaviour
    {
        protected PowerUpClass currentPowerUP;

        [Header("Parameters")]
        [SerializeField] protected float speed;
        [SerializeField] protected float expirationTime;
        [SerializeField] protected float valueMultiplier;
        [field: SerializeField] public PowerUpType PowerUpType { get; protected set; } = PowerUpType.Coin;

        private void SetNewPowerUp(PlayerManager playerManager)
        {
            if (CheckIfSamePowerUpType(playerManager))
            {
                IncreaseCurrentParameters(expirationTime * 0.5f, playerManager);
                Destroy(gameObject, 3.0f);
                return;
            }
            currentPowerUP = playerManager.currentPowerUp;
        }

        private void FixedUpdate()
        {
            transform.position += speed * Time.deltaTime * Vector3.down;
        }

        public virtual void ApplyPowerUp(PlayerManager playerManager)
        {
            SetNewPowerUp(playerManager);
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }

        public virtual void EndPowerUp(PlayerManager playerManager)
        {
            playerManager.currentPowerUp = null;
            playerManager.expirationTime = 0.0f;
        }

        protected void SetParameters(PlayerManager playerManager)
        {
            playerManager.currentPowerUp = this;
            playerManager.expirationTime = expirationTime;
        }

        public bool CheckIfSamePowerUpType(PowerUpType type)
        {
            return PowerUpType.Equals(type);
        }

        protected bool CheckIfSamePowerUpType(PlayerManager playerManager)
        {
            return (playerManager.currentPowerUp != null && playerManager.currentPowerUp.PowerUpType.Equals(PowerUpType));
        }

        protected void IncreaseCurrentParameters(float expireTime, PlayerManager playerManager)
        {
            playerManager.expirationTime += expireTime;
        }
    }
}
