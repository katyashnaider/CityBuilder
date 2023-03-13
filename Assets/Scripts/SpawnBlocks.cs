using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    [SerializeField] private BlockPool _pool;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private float _maxBlocksOnBelt = 24f;

    private List<Block> _actibBlocks = new List<Block>();
    private float _lastSpawnTime;

    private void Update()
    {
        if (_actibBlocks.Count >= _maxBlocksOnBelt) 
                return;

        if (Time.time - _lastSpawnTime >= _spawnInterval)
        {
            Block block = _pool.Get();
            _actibBlocks.Add(block);

            _lastSpawnTime = Time.time;
        }
    }
}