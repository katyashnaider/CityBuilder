using System;
using UnityEngine;
using UnityEngine.Serialization;
using Worker.StateMachines;
using Worker.StateMachines.States;

namespace Worker
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private Transform[] _targets;
        [SerializeField] private StoneStorage _stoneStorage;
        [SerializeField] private Building _building;
        [SerializeField] private Stone _heldStone;
        [SerializeField] private float _speed = 0.4f;
        
        private Transform _target;
        private StateMachine _stateMachine;
        private Animator _animator;
        
        private int _currentTargetIndex = 0;
        private bool _isStoneTaken = false;
        private bool _isStoneTaken1;

        public float Speed => _speed;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _stateMachine = new StateMachine();

            var walking = new Walking(this);
            var takingStone = new TakingStone(this, _animator);
            var putsStone = new PutsStone(this, _animator);
            
            //из Walking в TakingStone
            //из TakingStone в PutsStone
            //из PutsStone в Walking
            
            _stateMachine.AddTransition(walking, takingStone, ReachedPointA);
            _stateMachine.AddTransition(walking, putsStone, ReachedPointB);
            _stateMachine.AddTransition(takingStone, walking, () => _isStoneTaken);
            _stateMachine.AddTransition(putsStone, walking, () => !_isStoneTaken);

            _target = _targets[_currentTargetIndex];
            _stateMachine.SetState(walking);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        public void SetTarget(ref Transform target)
        {
            //Debug.Log("работает класс Worker метод SetTarget");
            target = _target;
        }

        public void SwitchTarget()
        {
            //Debug.Log("работает класс Worker метод SwitchTarget");
            //Debug.Log(_currentTargetIndex);
            _currentTargetIndex++;

            if (_currentTargetIndex >= _targets.Length)
                ResetTargetIndex();

            _target = _targets[_currentTargetIndex];
        }

        public void TakeStone(bool isStoneVisible) //виден ли камень
        {
            _heldStone.gameObject.SetActive(isStoneVisible);
            _stoneStorage.RemoveStone();
            _isStoneTaken = true;
            //Debug.Log("После " + _stoneStorage._listStones.Count);
        }

        public void PutStone(bool isStoneVisible) //виден ли камень
        {
            _heldStone.gameObject.SetActive(isStoneVisible);
            _building.GetStone();
            _isStoneTaken = false;
        }
        
        private void ResetTargetIndex()
        {
            //Debug.Log("работает класс Worker метод ResetTargetIndex");
            _currentTargetIndex = 0;
        }

        /*private bool ReachedPoint(Vector3 point, bool isStoneTaken)
        {
            float distance = Vector3.Distance(transform.position, point);

            if (distance <= 1f)
            {
                return true;
            }

            return false;
        }*/

        private bool ReachedPointA()
        {
            //return ReachedPoint(targetPointA);
            float distance = Vector3.Distance(transform.position, _targets[0].position);

            if (distance <= 0.2f && !_isStoneTaken)
            {
                _isStoneTaken1 = true;
                return true;
            }

            return false;
        }
        
        private bool ReachedPointB()
        {
            //return ReachedPoint(targetPointB);
            
            float distance = Vector3.Distance(transform.position, _targets[1].position);

            if (distance <= 0.2f && _isStoneTaken1)
            {
               _isStoneTaken1 = false;
                return true;
            }

            return false;
        }
    }
}