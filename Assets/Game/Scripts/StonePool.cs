using UnityEngine;

public class StonePool : ObjectPool<Stone>
{
    [SerializeField] private Stone[] _prefabs;

    private int _countCreated;
    private int _nextStoneIndex = 0;

    protected override Stone CreateObject()
    {
        var newStone = Instantiate(_prefabs[_nextStoneIndex], transform);
        _nextStoneIndex = (_nextStoneIndex + 1) % _prefabs.Length;

        return newStone;
    }
}