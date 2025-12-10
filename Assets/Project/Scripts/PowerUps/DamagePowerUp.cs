namespace Onion_AI
{
    public class DamagePowerUp : PowerUpClass
    {
        public override void ApplyPowerUp(PlayerManager playerManager)
        {
            base.ApplyPowerUp(playerManager);
            PlayerCombat playerCombat = playerManager.Combat;

            if(CheckIfSamePowerUpType(playerManager))
            {
                //Add Bullet Spread
                playerCombat.currentDamageModifier += valueMultiplier * 0.75f;
                return;
            }
            SetParameters(playerManager);
            playerCombat.currentDamageModifier = valueMultiplier;
        }

        public override void EndPowerUp(PlayerManager playerManager)
        {
            base.EndPowerUp(playerManager);
            PlayerCombat playerCombat = playerManager.Combat;
            playerCombat.currentDamageModifier = playerCombat.damageModifier;
        }
    }
}
