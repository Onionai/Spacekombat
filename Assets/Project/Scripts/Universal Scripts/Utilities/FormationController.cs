using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class FormationController : MonoBehaviour
    {
        [Header("General Stats")]
        [SerializeField] private float time = 0.5f;
        [SerializeField] protected float _spread = 1;
        [SerializeField] private float _nthOffset = 0;
        [SerializeField] [Range(0, 1)] protected float _noise = 0;

        [Header("Box Stats")]
        [SerializeField] private int _formationDepth = 5;
        [SerializeField] private int _formationWidth = 5;

        [Header("Radial Stats")]
        [SerializeField] private float _radius = 1;
        [SerializeField] private int _rotations = 1;
        [SerializeField] private float _ringOffset = 1;
        [SerializeField] private float _radiusGrowthMultiplier = 0;

        [Header("Status")]
        public FormationType formationType = FormationType.Box;

        public void Initialize(int depth, int width)
        {
            _formationWidth = (formationType == FormationType.Box) ? 6 : width;
            _formationDepth = (formationType == FormationType.Box) ? depth / 6 : depth;

            RandomizeParameters();
        }

        private void RandomizeParameters()
        {
            _noise = Random.Range(0f, 0.35f);

            if(formationType == FormationType.Box)
            {
                _spread = Random.Range(0.75f, 0.8f);
            }
            else
            {
                _rotations = Random.Range(1, 3);
                _radius = (_formationDepth == 1) ? Random.Range(2f, 2.75f) : Random.Range(1.45f, 1.851f);

                _ringOffset = Random.Range(0.85f, 1f);
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
            _nthOffset = GetNextNthOffset();

            if(formationType == FormationType.Box)
            {
                return BoxEvaluation();
            }
            return RadialEvaluation();
        }

        private float GetNextNthOffset()
        {
            if(formationType == FormationType.Circle)
            {
                if(_formationDepth <= 1)
                {
                    return 0;
                }
            }

            float pingTime = Time.time * time;
            return Mathf.PingPong(pingTime, 1f);
        }

        private IEnumerable<Vector3> BoxEvaluation()
        {
            var middleOffset = new Vector3(_formationWidth * 0.5f, _formationDepth * 0.5f, 0);

            for (var x = 0; x < _formationWidth; x++)
            {
                for (var y = 0; y < _formationDepth; y++)
                {
                    // Determine half-row index and its default direction
                    var rowHalfIndex = y / 2;
                    float direction = (rowHalfIndex % 2 == 0) ? 1 : -1; // Even rows go right, odd rows go left

                    // Introduce random variation: occasionally flip the direction
                    if (Random.value > 0.7f)  // 30% chance to flip direction
                    {
                        direction *= -1;  // Flip the direction
                    }

                    // Get the nthOffset for this row, but only update it once per full pass
                    var pos = new Vector3(x + (y % 2 == 0 ? 0 : _nthOffset * direction), y, 0);

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

