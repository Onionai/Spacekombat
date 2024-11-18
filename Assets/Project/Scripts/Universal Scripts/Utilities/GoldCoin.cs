using UnityEngine;

namespace Onion_AI
{
    public class GoldCoin : MonoBehaviour, IReleaseFromPool
    {
        public LevelSpawners levelSpawner;

        [field: Header("Coin Stats")]
        [SerializeField] private int coinCount;
        [SerializeField] private float acceleration = 10;
        [SerializeField] private float movementSpeed = 100;

        public void SetCoinCount(int count)
        {
            coinCount = count;
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;
            HandleMovement(delta);
        }

        private void HandleMovement(float delta)
        {
            float speed = acceleration * movementSpeed * delta;
            transform.position = Vector3.MoveTowards(transform.position, GameManager.playerTransform.position, speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                return;
            }
            PlayerManager playerManager = other.GetComponentInParent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.coinCount += coinCount;
                ReleaseFromPool();
            }
        }

        public void ReleaseFromPool()
        {
            levelSpawner.goldObjectPool.Release(this);
        }
    }
}
