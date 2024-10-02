using UnityEngine;
using System.Collections.Generic;

namespace Tarodev_Formations
{
    public class BoxFormation : FormationBase 
    {
        [SerializeField] private int _unitWidth = 5;
        [SerializeField] private int _unitDepth = 5;
        [SerializeField] private bool _hollow = false;
        [SerializeField] private float _nthOffset = 0;

        public override void RandomizeParameters()
        {
            int random = Random.Range(0, 6);

            _hollow = (random >= 5);
            _noise = Random.Range(0f, 1f);
            _nthOffset = Random.Range(0, 1f);
            _spread = Random.Range(0.75f, 2f);
        }

        public override void Initialize(int depth, int width)
        {
            _unitDepth = depth;
            _unitWidth = width;
        }

        public override IEnumerable<Vector3> EvaluatePoints() 
        {
            var middleOffset = new Vector3(_unitWidth * 0.5f, 0, _unitDepth * 0.5f);

            for (var x = 0; x < _unitWidth; x++) 
            {
                for (var z = 0; z < _unitDepth; z++) 
                {
                    if (_hollow && x != 0 && x != _unitWidth - 1 && z != 0 && z != _unitDepth - 1) continue;
                    var pos = new Vector3(x + (z % 2 == 0 ? 0 : _nthOffset), 0, z);

                    pos -= middleOffset;

                    pos += GetNoise(pos);

                    pos *= _spread;

                    yield return pos;
                }
            }
        }
    }
}