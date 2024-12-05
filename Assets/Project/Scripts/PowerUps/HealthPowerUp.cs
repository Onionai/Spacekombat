namespace Onion_AI
{
    public class HealthPowerUp : PowerUpClass
    {
        protected override void SetPowerUp(PlayerManager playerManager)
        {
            base.SetPowerUp(playerManager);
        }

        public override void ApplyPowerUp(PlayerManager playerManager)
        {
            base.ApplyPowerUp(playerManager);

            SetParameters(playerManager);
            playerManager.playerStatistic.IncreaseCurrentHealth(valueMultiplier);

            EndPowerUp(playerManager);
        }
    }
}
