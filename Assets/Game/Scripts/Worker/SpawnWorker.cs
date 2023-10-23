using System;
using UnityEngine;
using Workers;
using Workers.StateMachines.States;

namespace Scripts
{
    public class SpawnWorker : RestartEntity
    {
        [SerializeField] private FactoryWorker _factoryWorker;

        private Worker _worker;
        
        private void Start()
        {
            _worker = _factoryWorker.CreateWorker(transform.position, transform.rotation);
            _worker.StateMachine.SetState<Walking>();
        }
        
        public override void Restart()
        {
            _worker.transform.position = transform.position;
            _worker.transform.rotation = transform.rotation;
        }
    }
}