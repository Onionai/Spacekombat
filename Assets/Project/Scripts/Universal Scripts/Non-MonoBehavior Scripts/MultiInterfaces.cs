using UnityEngine;

namespace Onion_AI
{
    public interface IObjectSpawner
    {
        public void IObjectSpawner_SpawnObject(SpawnPoint spawnPoint);
    }

    public interface IReleaseFromPool
    {
        public void ReleaseFromPool();
    }
}
