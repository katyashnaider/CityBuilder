using System;
using UnityEngine;
using Worker.StateMachines;
using Worker.StateMachines.States;

namespace Worker
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private Transform[] _targets;
        [SerializeField] private Stone _stoneInHands;
        [SerializeField] private StoneStorage _stoneStorage;
        [SerializeField] private float _speed = 3;
        
        private Transform _target;
        private StateMachine _stateMachine;
        private int _currentTargetIndex = 0;

        public float Speed => _speed;
        
        private void Awake()
        {
            //сделующее состояние - взять камень+
            //далее - нести камень (?) но походу оно не нужно
            //далее - класть камень
            _stateMachine = new StateMachine();

            var walking = new Walking(this);
            var takingStone = new TakingStone(this);

            _stateMachine.SetState(walking);

            _target = _targets[_currentTargetIndex];
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

        public void TakeStone()
        {
            _stoneInHands.gameObject.SetActive(true);
            _stoneStorage.RemoveStone();
            //Debug.Log("После " + _stoneStorage._listStones.Count);
        }
        
        private void ResetTargetIndex()
        {
            //Debug.Log("работает класс Worker метод ResetTargetIndex");
            _currentTargetIndex = 0;
        }
    }
}