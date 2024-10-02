using UnityEngine;

namespace Onion_AI
{
    public class GoldCoin : MonoBehaviour, IReleaseFromPool
    {
        public LevelSpawners levelSpawner;

        [field: Header("Player Stats")]
        [SerializeField] private float acceleration = 10;
        [SerializeField] private float movementSpeed = 100;
        [SerializeField] private float rotationSpeed = 20.0f;

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;
            HandleMovement(delta);
        }

        private void HandleMovement(float delta)
        {
            float speed = acceleration * movementSpeed * delta;
            transform.position += Vector3.down * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerManager playerManager = other.GetComponentInParent<PlayerManager>();
            if(playerManager != null)
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
