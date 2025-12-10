using UnityEngine;

namespace Onion_AI
{
    public class Boundaries : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent<IReleaseFromPool>(out var poolReleaser))
            {
                poolReleaser.ReleaseFromPool();
            }

            if (other.CompareTag("PowerUp"))
            {
                Destroy(gameObject, 1.0f);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            IReleaseFromPool poolReleaser = other.GetComponentInParent<IReleaseFromPool>();
            poolReleaser?.ReleaseFromPool();
        }
    }
}
