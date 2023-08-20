using DG.Tweening;
using UnityEngine;

namespace Worker.StateMachines.States
{
    public class Walking : IState
    {
        private readonly Worker _worker;
        private Vector3[] _pathTargets;
        private bool _isWalking = false;
        private Tweener _pathTweener;

        public Walking(Worker worker)
        {
            _worker = worker;
        }

        public void OnEnter()
        {
            GetTarget();
        }

        public void Tick()
        {
            if (!_isWalking)
            {
                MoveOnPath();
            }
        }

        public void OnExit()
        {
            _isWalking = false;
        }

        private void GetTarget()
        {
            _pathTargets = _worker.SetTarget();
        }

        private void MoveOnPath()
        {
            if (_pathTweener != null) return;
            
            _isWalking = true;
            
            _pathTweener = _worker.transform.DOPath(_pathTargets, _worker.Speed, PathType.Linear, PathMode.Full3D).SetLookAt(0.01f)
                .SetEase(Ease.Linear).OnKill(OnPathKill);
        }

        private void OnPathKill()
        {
            _isWalking = false;
            _pathTweener = null;
        }
    }
}