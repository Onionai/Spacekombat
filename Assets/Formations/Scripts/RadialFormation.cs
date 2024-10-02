using UnityEngine;
using System.Collections.Generic;

namespace Tarodev_Formations
{
    public class RadialFormation : FormationBase 
    {
        [SerializeField] private int _amount = 10;
        [SerializeField] private float _radius = 1;
        [SerializeField] private float _radiusGrowthMultiplier = 0;
        [SerializeField] private float _rotations = 1;
        [SerializeField] private int _rings = 1;
        [SerializeField] private float _ringOffset = 1;
        [SerializeField] private float _nthOffset = 0;

        public override void RandomizeParameters()
        {
            _noise = Random.Range(0f, 1f);
            _radius = Random.Range(0.45f, 2f);
            _rotations = Random.Range(0.5f, 2.5f);

            _nthOffset = Random.Range(0f, 1f);
            _spread = Random.Range(0.75f, 2f);
            _ringOffset = Random.Range(0.55f, 2.5f);
            _radiusGrowthMultiplier = Random.Range(0,1);
        }

        public override void Initialize(int amount, int rings)
        {
            _rings = rings;
            _amount = amount;
        }

        public override IEnumerable<Vector3> EvaluatePoints() 
        {
            var amountPerRing = _amount / _rings;
            var ringOffset = 0f;
            for (var i = 0; i < _rings; i++) 
            {
                for (var j = 0; j < amountPerRing; j++) 
                {
                    var angle = j * Mathf.PI * (2 * _rotations) / amountPerRing + (i % 2 != 0 ? _nthOffset : 0);

                    var radius = _radius + ringOffset + j * _radiusGrowthMultiplier;
                    var x = Mathf.Cos(angle) * radius;
                    var z = Mathf.Sin(angle) * radius;

                    var pos = new Vector3(x, 0, z);

                    pos += GetNoise(pos);

                    pos *= _spread;

                    yield return pos;
                }

                ringOffset += _ringOffset;
            }
        }
    }
}