namespace Onion_AI
{
    public class DamagePowerUp : PowerUpClass
    {
        private DamagePowerUp damagePowerUp;
        private bool currentPowerUpIsDamage;

        protected override void SetPowerUp(PlayerManager playerManager)
        {
            base.SetPowerUp(playerManager);

            damagePowerUp = currentPowerUP as DamagePowerUp;
            currentPowerUpIsDamage = (damagePowerUp != null);
        }

        public override void ApplyPowerUp(PlayerManager playerManager)
        {
            base.ApplyPowerUp(playerManager);
            PlayerCombat playerCombat = playerManager.playerCombat;

            if (currentPowerUpIsDamage)
            {
                IncreaseCurrentParameters(expirationTime * 0.5f, playerManager);
                playerCombat.currentDamageModifier += valueMultiplier * 0.75f;
                return;
            }

            SetParameters(playerManager);
            playerCombat.currentDamageModifier = valueMultiplier;
        }

        public override void EndPowerUp(PlayerManager playerManager)
        {
            base.EndPowerUp(playerManager);
            PlayerCombat playerCombat = playerManager.playerCombat;
            playerCombat.currentDamageModifier = playerCombat.damageModifier;
        }
    }
}
