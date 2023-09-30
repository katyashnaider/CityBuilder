﻿using DG.Tweening;
using UnityEngine;

namespace Workers.StateMachines.States
{
    public class Walking : IState
    {
        private readonly Worker _worker;
        private readonly Animator _animator;
        
        private Vector3 _point;
        private bool _isWalking = false;

        public Walking(Worker worker, Animator animator)
        {
            _worker = worker;
            _animator = animator;
        }

        public void OnEnter()
        {
            SetTarget();
            _isWalking = true;
        }

        public void Tick()
        {
            if (_isWalking)
            {
                MoveOnPoint();
            }
        }

        public void OnExit()
        {
            _isWalking = false;
            _worker.SwitchTarget();
        }

        private void SetTarget()
        {
            _point = _worker.GetTarget();
        }

        private void MoveOnPoint()
        {
            _worker.transform.position = Vector3.MoveTowards(_worker.transform.position, _point, _worker.Speed * Time.deltaTime);
            //_animator.SetTrigger("Turn");
            _worker.transform.LookAt(_point);
            _worker.ReachedPoint();
        }
    }
}