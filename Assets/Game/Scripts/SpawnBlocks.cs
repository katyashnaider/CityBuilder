using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnBlocks : MonoBehaviour
{
    [SerializeField] private StonePool _pool;
    [SerializeField] private float _spawnInterval = 1f; 
    [SerializeField] private float _maxStonesOnBelt = 24f;

    private List<Stone> _activStones = new List<Stone>();
    private float _lastSpawnTime;

    private void Update()
    {
        if (_activStones.Count >= _maxStonesOnBelt) 
                return;

        if (Time.time - _lastSpawnTime >= _spawnInterval)
        {
            Stone stone = _pool.Get();
            _activStones.Add(stone);

            _lastSpawnTime = Time.time;
        }
    }
}