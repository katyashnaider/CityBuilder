using CityBuilder.Workers.StateMachine.States;
using UnityEngine;

namespace CityBuilder.Workers
{
    public class SpawnWorker : MonoBehaviour
    {
        [SerializeField] private FactoryWorker _factoryWorker;

        private void Start()
        {
            Worker worker = _factoryWorker.CreateWorker(transform.position, transform.rotation);
            worker.StateMachine.SetState<Walking>();
        }
    }
}