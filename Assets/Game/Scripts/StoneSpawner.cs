using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private StonePool _pool;
    [SerializeField] private Transform[] _positionsStones;

    private int _countStoneSpawn;
    private int _activeStone;

    private readonly List<Stone> _stones = new List<Stone>();

    private void Start()
    {
        _countStoneSpawn = _positionsStones.Length;

        for (int i = 0; i < _countStoneSpawn; i++)
        {
            Stone stone = _pool.Get();
            stone.transform.position = _positionsStones[i].position;
            _stones.Add(stone);
        }

        for (int i = 0; i < _stones.Count / 2; i++)
        {
            _pool.Put(_stones[i]);
        }
    }
    
    public void OnIncludedStone()
    {
        Stone notActiveStone = _stones.FirstOrDefault(stone => stone.gameObject.activeSelf == false);

        if (_activeStone <= _countStoneSpawn && notActiveStone != null)
        {
            notActiveStone.gameObject.SetActive(true);
            UpdateActiveStoneCount();
        }
    }

    public void InActiveStone()
    {
        UpdateActiveStoneCount();

        if (_stones.Count > 0 && _activeStone > 0)
        {
            Stone activeStone = _stones.FirstOrDefault(stone => stone.gameObject.activeSelf);
            _pool.Put(activeStone);
        }
        else
        {
            Debug.LogError("No stones left in the storage!");
        }
    }

    private int UpdateActiveStoneCount() => _activeStone = _stones.Count(stone => stone.gameObject.activeSelf);
}