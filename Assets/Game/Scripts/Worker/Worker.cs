using System;
using UnityEngine;
using UnityEngine.Serialization;
using Upgrades;
using Worker.StateMachines;
using Worker.StateMachines.States;

namespace Worker
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Wallet))]
    public class Worker : MonoBehaviour
    {
        //создать класс Кошелек и там хранить деньги и проверять хватает ли денег +
        //создать класс Игрок и перенсти логику с Рабочего -
        //визуализировать то, когда камушки кладутся
        // сделать отдельно каждые колонки и стенки. создать класс который будет показывать количество монет (типо анимация через дотвин)+
        //переделать управление камерой (см класс камеры)

        [SerializeField] private SpeedUpgrade _speedUpgrade;
       // [SerializeField] private BuildingPart _buildingPart;
        [SerializeField] private Transform[] _targets;
       // [SerializeField] private StoneStorage _stoneStorage;
        [SerializeField] private StoneSpawner _stoneSpawner;
        [SerializeField] private Building _building;
        [SerializeField] private Stone _heldStone;
        [SerializeField] private float _speed = 0.4f;
        [SerializeField] private int _currentPrice = 5;

        private Transform _target;
        private StateMachine _stateMachine;
        private Wallet _wallet;
        private Animator _animator;

        private int _currentTargetIndex = 0;

        //private int _wallet = 0;
        private bool _isStoneTaken = false;
        private bool _isStoneTaken1;
        private Vector3[] _pathTargets;
        private float _currentSpeed;

        public float Speed => _speed;

        private void Awake()
        {
            Screen.SetResolution(1080, 1920, true);
            _currentSpeed = _speed;

            _animator = GetComponent<Animator>();
            _wallet = GetComponent<Wallet>();

            _stateMachine = new StateMachine();

            var walking = new Walking(this);
            var takingStone = new TakingStone(this, _animator);
            var putsStone = new PutsStone(this, _animator);

            ConvertTargetsToPath();

            _stateMachine.AddTransition(walking, takingStone, ReachedPointA);
            _stateMachine.AddTransition(walking, putsStone, ReachedPointB);
            _stateMachine.AddTransition(takingStone, walking, () => _isStoneTaken);
            _stateMachine.AddTransition(putsStone, walking, () => !_isStoneTaken);

            _stateMachine.SetState(walking);
        }

        private void OnEnable()
        {
            _building.DeliveredStone += OnDeliveredStone;
            _speedUpgrade.ChangedSpeed += OnChangedSpeed;
        }

        private void OnDisable()
        {
            _building.DeliveredStone -= OnDeliveredStone;
            _speedUpgrade.ChangedSpeed -= OnChangedSpeed;
        }

        private void Update() => _stateMachine.Tick();

        /*public void SetTarget(Vector3[] pathTargets)
        {
            //Debug.Log("работает класс Worker метод SetTarget");
            _pathTargets = pathTargets;
        }*/

        public Vector3[] SetTarget()
        {
            return _pathTargets;
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
            // _stoneStorage.RemoveStone();
            _stoneSpawner.InActiveStone();
            _isStoneTaken = true;
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

        private void OnDeliveredStone()
        {
            _wallet.AddCoins(_currentPrice);
            _building.SetCurrentPrice(_currentPrice);
        }

        private void OnChangedSpeed(float upgrateAmount)
        {
            if (_speed - upgrateAmount > 0)
                _speed -= upgrateAmount;
        }

        private void ConvertTargetsToPath()
        {
            _pathTargets = new Vector3[_targets.Length];

            for (int i = 0; i < _targets.Length; i++)
            {
                _pathTargets[i] = _targets[i].position;
            }
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