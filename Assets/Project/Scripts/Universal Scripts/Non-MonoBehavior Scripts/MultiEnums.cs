namespace Onion_AI
{
    public enum PowerUpType
    {
        Coin,
        Health,
        Shield,
        Damage
    }

    public enum TaskType
    {
        Socials,
        Referral,
        KillEnemies
    }
    public enum TaskRewards
    {
        Both,
        Coin,
        Experience
    }
    
    public enum CharacterType
    {
        Enemy,
        Player
    }

    public enum FormationType { Box, Circle }

    public enum GamePlayState { Active, PlayerPause, PlayerResume, PlayerDead, MissionComplete, SpawningEnemy }

    public enum BossFiringType
    {
        Static,
        Singular,
        Continuos
    }

    public enum EnemyType
    {
        FreeFall,
        FreeRoam,
        Formation
    }
}
