using TMPro;
using UnityEngine;
using Workers;

namespace Pool
{
    public class WorkerPool : ObjectPool<Worker>
    {
        [SerializeField] private Worker _prefab;
        
        protected override Worker CreateObject() => Instantiate(_prefab, transform);
    }
}