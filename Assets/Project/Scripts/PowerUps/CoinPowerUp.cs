namespace Onion_AI
{
    public class CoinPowerUp : PowerUpClass
    {
        public override void ApplyPowerUp(PlayerManager playerManager)
        {
            base.ApplyPowerUp(playerManager);
            playerManager.HandleCoinMagnetism(true);
        }

        public override void EndPowerUp(PlayerManager playerManager)
        {
            base.EndPowerUp(playerManager);
            playerManager.HandleCoinMagnetism(false);
        }
    }
}
