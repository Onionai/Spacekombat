using UnityEngine;

namespace Onion_AI
{
    public class GoldCoin : MonoBehaviour, IReleaseFromPool
    {
        public LevelSpawners levelSpawner;
        private PlayerManager playerManager;

        [field: Header("Coin Stats")]
        [SerializeField] private int coinCount;
        [SerializeField] private float acceleration = 10;
        [SerializeField] private float movementSpeed = 100;

        public void CreateCoin(int count, PlayerManager player)
        {
            coinCount = count;
            playerManager = player;
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;
            HandleMovement(delta);
        }

        private void HandleMovement(float delta)
        {
            float speed = acceleration * movementSpeed * delta;

            if (playerManager != null)
            {
                if (playerManager.HasMagnet)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerManager.transform.position, speed);
                    return;
                }
            }
            transform.position += Vector3.down * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                return;
            }

            if (playerManager != null)
            {
                playerManager.coinCount++;
                ReleaseFromPool();
            }
        }

        public void ReleaseFromPool()
        {
            levelSpawner.goldObjectPool.Release(this);
        }
    }
}
