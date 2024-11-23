namespace Onion_AI
{
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

    public enum GameState
    {
        Active,
        Paused,
        Resume,
        Failed
    }

    public enum FormationType
    {
        Box,
        Circle
    }

    public enum BossFiringType
    {
        Static,
        Singular,
        Continuos
    }

    public enum EnemyType
    {
        Static,
        Linear,
        FreeRoam
    }
    
    public enum MissionStatus
    {
        Active,
        Failed,
        Completed
    }
}
