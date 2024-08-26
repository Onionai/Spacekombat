using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onion_AI
{
    public interface IObjectSpawner
    {
        public void IObjectSpawner_SpawnObject(float minWidth, float maxWidth);
    }

    public interface IReleaseFromPool
    {
        public void ReleaseFromPool();
    }
}
