﻿using System;
using UnityEngine;
using Workers;
using Workers.StateMachines.States;

namespace Game.Scripts
{
    public class SpawnWorker : MonoBehaviour
    {
        [SerializeField] private FactoryWorker _factoryWorker;

        private void Start()
        {
            var worker = _factoryWorker.CreateWorker(transform.position, transform.rotation);
            worker.StateMachine.SetState<Walking>();
        }
    }
}