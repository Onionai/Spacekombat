using UnityEngine;

namespace Onion_AI
{
    public class PowerUpClass : MonoBehaviour, IReleaseFromPool
    {
        protected PowerUpClass currentPowerUP;

        [Header("Parameters")]
        [SerializeField] protected float speed;
        [SerializeField] protected float expirationTime;
        [SerializeField] protected float valueMultiplier;
        [field: SerializeField] public PowerUpType powerUpType { get; protected set; } = PowerUpType.Coin;

        protected virtual void SetPowerUp(PlayerManager playerManager)
        {
            currentPowerUP = playerManager.currentPowerUp;
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        public void ReleaseFromPool()
        {
            Destroy(gameObject, 0.2f);
        }

        public virtual void ApplyPowerUp(PlayerManager playerManager)
        {
            SetPowerUp(playerManager);
            gameObject.SetActive(false);
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

        protected void IncreaseCurrentParameters(float expireTime, PlayerManager playerManager)
        {
            playerManager.currentPowerUp = this;
            playerManager.expirationTime += expireTime;
        }
    }
}
