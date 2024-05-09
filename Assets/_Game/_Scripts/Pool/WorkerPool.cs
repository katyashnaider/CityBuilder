using CityBuilder.Workers;
using UnityEngine;

namespace CityBuilder.Pool
{
    public class WorkerPool : ObjectPool<Worker>
    {
        [SerializeField] private Worker[] _prefabs;

        private int _index = 0;

        protected override Worker CreateObject()
        {
            var worker = Instantiate(_prefabs[_index], transform);
            _index = (_index + 1) % _prefabs.Length;
            return worker;
        }
    }
}