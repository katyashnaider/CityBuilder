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
        private bool _isStoneVisible; //виден ли камень

        public TakingStone(Worker worker, Animator animator)
        {
            _worker = worker;
            _animator = animator;
        }

        public void OnEnter()
        {
            _animator.SetBool(HashAnimator.IsStoneTaken, true);
            _isStoneVisible = true;
            _worker.TakeStone(_isStoneVisible);
        }
        
        public void Tick()
        {
        }

        public void OnExit()
        {
            //_isStoneVisible = false;
            //_worker.TakeStone(_isStoneVisible);
        }
    }
}