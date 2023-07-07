using System;
using DG.Tweening;
using UnityEngine;

namespace Worker.StateMachines.States
{
    public class Walking : IState
    {
        private readonly Worker _worker;
        private Transform _target;
        private Animator _animator;

        private const float POSSIBLE_ERROR = 0.5f;

        public Walking(Worker worker /*, Animator animator*/)
        {
            _worker = worker;
            //_animator = animator;
        }

        public void OnEnter()
        {
            //_worker = GetComponent<Worker>();
            GetTarget();
        }

        public void Tick()
        {
            //включить анимацию ходьбы
            GetTarget();
            //разобраться и возможно упростить
            Vector3 direction = _target.position - _worker.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            float duration = GetDistanceToTarget() / _worker.Speed;
            float rotationDuration = 0.3f;

            _worker.transform.DOMove(_target.position, duration).SetEase(Ease.Linear).OnComplete(OnMovementComplete);
            _worker.transform.DORotateQuaternion(targetRotation, rotationDuration);
        }

        public void OnExit()
        {
        }

        private float GetDistanceToTarget()
        {
            return Vector3.Distance(_worker.transform.position, _target.position);
        }

        private void GetTarget()
        {
            _worker.SetTarget(ref _target);
        }

        private void OnMovementComplete()
        {
            if (GetDistanceToTarget() <= POSSIBLE_ERROR)
                _worker.SwitchTarget();
        }

        /*private void Start()
       {
           _worker = GetComponent<Worker>();
           GetTarget();
       }*/


        /*private void Update()
        {
            GetTarget();

            Debug.Log(_target.position);

            transform.DOMove(_target.position, _movementDuration).SetEase(Ease.Flash).OnComplete(OnMovementComplete);

        }*/
    }
}