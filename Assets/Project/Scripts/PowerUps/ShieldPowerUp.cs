namespace Onion_AI
{
    public class ShieldPowerUp : PowerUpClass
    {
        public override void ApplyPowerUp(PlayerManager playerManager)
        {
            base.ApplyPowerUp(playerManager);
            playerManager.HandleShieldVisibility(true);

            if (CheckIfSamePowerUpType(playerManager))
            {
                return;
            }
            SetParameters(playerManager);
        }

        public override void EndPowerUp(PlayerManager playerManager)
        {
            base.EndPowerUp(playerManager);
            playerManager.HandleShieldVisibility(false);
        }
    }
}
