using System;
using UnityEngine;
using System.Collections.Generic;

namespace Tarodev_Formations
{
    public abstract class FormationBase : MonoBehaviour 
    {
        [SerializeField] [Range(0, 1)] protected float _noise = 0;
        [SerializeField] protected float _spread = 1;

        public virtual void RandomizeParameters()
        {
        
        }

        public virtual void Initialize(int a, int b)
        {

        }

        public abstract IEnumerable<Vector3> EvaluatePoints();

        public Vector3 GetNoise(Vector3 pos)
        {
            var noise = Mathf.PerlinNoise(pos.x * _noise, pos.z * _noise);
            return new Vector3(noise, 0, noise);
        }
    }
}