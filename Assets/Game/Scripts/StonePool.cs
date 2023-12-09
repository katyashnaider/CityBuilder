using UnityEngine;

namespace CityBuilder
{
    public class StonePool : ObjectPool<Stone>
    {
        [SerializeField] private Stone[] _prefabs;

        private int _countCreated;
        private int _nextStoneIndex;

        protected override Stone CreateObject()
        {
            Stone newStone = Instantiate(_prefabs[_nextStoneIndex], transform);
            _nextStoneIndex = (_nextStoneIndex + 1) % _prefabs.Length;

            return newStone;
        }
    }
}