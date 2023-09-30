using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Workers.StateMachines.States
{
    public class PutsStone : IState
    {
        private readonly Worker _worker;
        private bool _isStoneVisible;
        
        public PutsStone(Worker worker)
        {
            _worker = worker;
        }

        public void OnEnter()
        {
            _isStoneVisible = false;
            _worker.PutStone(_isStoneVisible);
        }

        public void Tick() { }

        public void OnExit() { }
    }
}