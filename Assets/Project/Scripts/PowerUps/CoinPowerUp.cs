namespace Onion_AI
{
    public class CoinPowerUp : PowerUpClass
    {
        private CoinPowerUp coinPowerUp;
        private bool currentPowerUpIsCoin;

        protected override void SetPowerUp(PlayerManager playerManager)
        {
            base.SetPowerUp(playerManager);

            coinPowerUp = currentPowerUP as CoinPowerUp;
            currentPowerUpIsCoin = (coinPowerUp != null);
        }

        public override void ApplyPowerUp(PlayerManager playerManager)
        {
            base.ApplyPowerUp(playerManager);

            if (currentPowerUpIsCoin)
            {
                playerManager.coinMultiplier += valueMultiplier * 0.5f;
                IncreaseCurrentParameters(expirationTime * 0.5f, playerManager);
                return;
            }
            SetParameters(playerManager);
            playerManager.coinMultiplier = valueMultiplier;
        }

        public override void EndPowerUp(PlayerManager playerManager)
        {
            playerManager.coinMultiplier = 1;
            base.EndPowerUp(playerManager);
        }
    }
}
