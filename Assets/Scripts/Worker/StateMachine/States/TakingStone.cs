using System;
using UnityEngine;

namespace Worker.StateMachines.States
{
    public class TakingStone : IState
    {
        //[SerializeField] private Stone _stone;
        //[SerializeField] private StoneStorage _stoneStorage;
        private Worker _worker;
        private Animator _animator;

        public TakingStone(Worker worker)
        {
            _worker = worker;
            //добавить аниматор
        }

        public void OnEnter()
        {
            _worker.TakeStone();
        }
        
        public void Tick()
        {
        }

        public void OnExit()
        {
        }
    }
}