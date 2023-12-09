using UnityEngine;

namespace CityBuilder.Pool
{
    public class WorkerPool : ObjectPool<Worker.Worker>
    {
        [SerializeField] private Worker.Worker _prefab;

        protected override Worker.Worker CreateObject()
        {
            return Instantiate(_prefab, transform);
        }
    }
}