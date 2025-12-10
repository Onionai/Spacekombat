namespace Onion_AI
{
    public class HealthPowerUp : PowerUpClass
    {
        public override void ApplyPowerUp(PlayerManager playerManager)
        {
            base.ApplyPowerUp(playerManager);

            SetParameters(playerManager);
            playerManager.Statistics.IncreaseCurrentHealth(valueMultiplier);

            EndPowerUp(playerManager);
        }
    }
}
