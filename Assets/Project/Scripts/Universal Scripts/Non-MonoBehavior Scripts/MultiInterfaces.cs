using UnityEngine;

namespace Onion_AI
{
    public interface IObjectSpawner
    {
        public void IObjectSpawner_SpawnObject(float minWidth, float maxWidth, SpawnPoint spawnPoint);
    }

    public interface IReleaseFromPool
    {
        public void ReleaseFromPool();
    }
}
