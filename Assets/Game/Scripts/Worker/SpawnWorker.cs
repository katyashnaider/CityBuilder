using CityBuilder.Worker.StateMachine.States;
using UnityEngine;

namespace CityBuilder.Worker
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