using UnityEngine;

namespace Onion_AI
{
    public class ShieldPowerUp : PowerUpClass
    {
        private Transform shield;
        private ShieldPowerUp shieldPowerUp;

        private bool currentPowerUpIsShield;

        [Header("Personal Parameters")]
        [SerializeField] private GameObject shieldObject;

        protected override void SetPowerUp(PlayerManager playerManager)
        {
            base.SetPowerUp(playerManager);

            shieldPowerUp = currentPowerUP as ShieldPowerUp;
            currentPowerUpIsShield = (shieldPowerUp != null);
        }

        public override void ApplyPowerUp(PlayerManager playerManager)
        {
            base.ApplyPowerUp(playerManager);

            if (currentPowerUpIsShield)
            {
                IncreaseCurrentParameters(expirationTime * 0.5f, playerManager);
                return;
            }

            SetParameters(playerManager);
            shield = Instantiate(shieldObject, playerManager.transform).transform;

            shield.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            shield.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public override void EndPowerUp(PlayerManager playerManager)
        {
            Destroy(shield.gameObject, 0.2f);
            base.EndPowerUp(playerManager);
        }
    }
}
