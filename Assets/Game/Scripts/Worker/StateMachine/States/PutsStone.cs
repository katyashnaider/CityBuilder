using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Worker.StateMachines.States
{
    public class PutsStone : IState
    {
        private Worker _worker;
        private Animator _animator;
        private bool _isStoneVisible; //виден ли камень
        
        private bool _isStoneTaken = false;

        public PutsStone(Worker worker, Animator animator)
        {
            _worker = worker;
            _animator = animator;
        }

        public void OnEnter()
        {
            _animator.SetBool(HashAnimator.IsStoneTaken, false);
            _isStoneVisible = false;
            _worker.PutStone(_isStoneVisible);
        }

        public void Tick()
        {
        }
        
        public void OnExit()
        {
            //_isStoneTaken = false;
            //_worker.PutStone(_isStoneVisible);        
        }
    }
}