using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class FormationController : MonoBehaviour
    {
        [Header("General Stats")]
        [SerializeField] protected float _spread = 1;
        [SerializeField] private float _nthOffset = 0;
        [SerializeField] [Range(0, 1)] protected float _noise = 0;

        [Header("Box Stats")]
        [SerializeField] private bool _hollow = false;
        [SerializeField] private int _formationDepth = 5;
        [SerializeField] private int _formationWidth = 5;

        [Header("Radial Stats")]
        [SerializeField] private float _radius = 1;
        [SerializeField] private float _rotations = 1;
        [SerializeField] private float _ringOffset = 1;
        [SerializeField] private float _radiusGrowthMultiplier = 0;

        [Header("Status")]
        public FormationType formationType = FormationType.Box;

        public void Initialize(int depth, int width)
        {
            _formationWidth = (formationType == FormationType.Box) ? 6 : width;
            _formationDepth = (formationType == FormationType.Box) ? depth / 6 : depth;
        }

        public void RandomizeParameters()
        {
            _noise = Random.Range(0f, 1f);
            _nthOffset = Random.Range(0, 1f);
            _spread = Random.Range(0.75f, 1.55f);

            if(formationType == FormationType.Box)
            {
                int random = Random.Range(0, 6);
                _hollow = random >= 5;
            }
            else
            {
                _radius = Random.Range(0.45f, 2f);
                _rotations = Random.Range(0.5f, 2.5f);

                _ringOffset = Random.Range(0.55f, 2.5f);
                _radiusGrowthMultiplier = Random.Range(0,1);
            }
        }

        public Vector3 GetNoise(Vector3 pos)
        {
            var noise = Mathf.PerlinNoise(pos.x * _noise, pos.z * _noise);
            return new Vector3(noise, 0, noise);
        }

        public IEnumerable<Vector3> EvaluatePoints() 
        {
            if(formationType == FormationType.Box)
            {
                return BoxEvaluation();
            }
            return RadialEvaluation();
        }

        private IEnumerable<Vector3> BoxEvaluation()
        {
            var middleOffset = new Vector3(_formationWidth * 0.5f, _formationDepth * 0.5f, 0);

            for (var x = 0; x < _formationWidth; x++) 
            {
                for (var z = 0; z < _formationDepth; z++) 
                {
                    if (_hollow && x != 0 && x != _formationWidth - 1 && z != 0 && z != _formationDepth - 1) continue;
                    var pos = new Vector3(x + (z % 2 == 0 ? 0 : _nthOffset), z, 0);

                    pos -= middleOffset;

                    pos += GetNoise(pos);

                    pos *= _spread;

                    yield return pos;
                }
            }
        }

        private IEnumerable<Vector3> RadialEvaluation()
        {
            var amountPerRing = _formationWidth / _formationDepth;
            var ringOffset = 0f;
            for (var i = 0; i < _formationDepth; i++) 
            {
                for (var j = 0; j < amountPerRing; j++) 
                {
                    var angle = j * Mathf.PI * (2 * _rotations) / amountPerRing + (i % 2 != 0 ? _nthOffset : 0);

                    var radius = _radius + ringOffset + j * _radiusGrowthMultiplier;
                    var x = Mathf.Cos(angle) * radius;
                    var y = Mathf.Sin(angle) * radius;

                    var pos = new Vector3(x, y, 0);

                    pos += GetNoise(pos);

                    pos *= _spread;

                    yield return pos;
                }

                ringOffset += _ringOffset;
            }
        }
    }
}

