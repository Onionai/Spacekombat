using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Tarodev_Formations
{
    public class ExampleArmy : MonoBehaviour 
    {
        private FormationBase formationBase;

        public FormationBase Formation 
        {
            get 
            {
                if (formationBase == null) 
                {
                    formationBase = GetComponent<FormationBase>();
                }
                return formationBase;
            }
            set => formationBase = value;
        }

        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private float _unitSpeed = 2;

        private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
        private List<Vector3> _points = new List<Vector3>();
        private Transform _parent;

        private void Awake() 
        {
            _parent = new GameObject("Unit Parent").transform;
        }

        private void Update() 
        {
            SetFormation();
        }

        private void SetFormation() 
        {
            _points = Formation.EvaluatePoints().ToList();

            if (_points.Count > _spawnedUnits.Count) 
            {
                var remainingPoints = _points.Skip(_spawnedUnits.Count);
                Spawn(remainingPoints);
            }
            else if (_points.Count < _spawnedUnits.Count) 
            {
                Kill(_spawnedUnits.Count - _points.Count);
            }

            for (var i = 0; i < _spawnedUnits.Count; i++) 
            {
                _spawnedUnits[i].transform.position = Vector3.MoveTowards(_spawnedUnits[i].transform.position, transform.position + _points[i], _unitSpeed * Time.deltaTime);
            }
        }

        private void Spawn(IEnumerable<Vector3> points) 
        {
            foreach (var pos in points) 
            {
                var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity, _parent);
                _spawnedUnits.Add(unit);
            }
        }

        private void Kill(int num) 
        {
            for (var i = 0; i < num; i++) 
            {
                var unit = _spawnedUnits.Last();
                _spawnedUnits.Remove(unit);
                unit.gameObject.SetActive(false);
            }
        }
    }
}