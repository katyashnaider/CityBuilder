using Scripts.Building;
using JetBrains.Annotations;
using Scripts;
using UnityEngine;
using Upgrades;
using Workers.StateMachines;

namespace Workers
{
    [RequireComponent(typeof(Animator))]
    public class Worker : RestartEntity
    {
        [SerializeField] private Stone _heldStone;

        private SpeedUpgrade _speedUpgrade;
        private IncomeUpgrade _incomeUpgrade;
        private Transform[] _targets;
        private Transform[] _pathPointsA;
        private Transform[] _pathPointsB;
        private BuildingController _buildingController;

        private Transform _target;
        private StateMachine _stateMachine;
        private Wallet _wallet;
        private Animator _animator;

        private int _currentPath = 0;

        private bool _isStoneTaken = false;
        private float _currentSpeed;
        private int _currentPrice;

        public Vector3 StartPosition { get; private set; }
        public float Speed => _currentSpeed;
        public StateMachine StateMachine => _stateMachine;
        public Animator Animator => _animator;
        public bool IsStoneTaken => _isStoneTaken;

        public void Init(StateMachine stateMachine, SpeedUpgrade speedUpgrade, IncomeUpgrade incomeUpgrade, Transform[] targets, [CanBeNull] Transform[] pathPointsA, Transform[] pathPointsB, BuildingController buildingController, Wallet wallet)
        {
            _speedUpgrade = speedUpgrade;
            _incomeUpgrade = incomeUpgrade;
            _targets = targets;
            _pathPointsA = pathPointsA;
            _pathPointsB = pathPointsB;
            _target = targets[0];
            _buildingController = buildingController;

            _stateMachine = stateMachine;

            StartPosition = transform.position;

            _animator = GetComponent<Animator>();
            _wallet = wallet;

            _buildingController.DeliveredStone += OnDeliveredStone;
            _speedUpgrade.ChangedSpeed += OnChangedSpeed;
            _incomeUpgrade.ChangedIncome += OnChangedIncome;
        }

        private void OnDisable()
        {
            _buildingController.DeliveredStone -= OnDeliveredStone;
            _speedUpgrade.ChangedSpeed -= OnChangedSpeed;
            _incomeUpgrade.ChangedIncome -= OnChangedIncome;
        }

        private void Update() => _stateMachine.Tick();
        
        public override void Restart()
        {
            _target = _targets[0];
            _currentPath = 0;
            _isStoneTaken = false;
        }

        public void SetStats(float speed, int price)
        {
            _currentSpeed = speed;
            _currentPrice = price;
        }

        public Vector3 GetTarget() => 
            _target.position;

        public void SwitchTarget()
        {
            _currentPath++;
            
            if (_currentPath > 1)
                _currentPath = 0;

            _target = _currentPath % 2 == 0 ? _pathPointsA[Random.Range(0, _pathPointsA.Length)] : _pathPointsB[Random.Range(0, _pathPointsB.Length)];
        }

        public void TakeStone(bool isStoneVisible)
        {
            _heldStone.gameObject.SetActive(isStoneVisible);
            _isStoneTaken = true;
        }

        public void PutStone(bool isStoneVisible)
        {
            _heldStone.gameObject.SetActive(isStoneVisible);
            _buildingController.GetStone();
            _isStoneTaken = false;
            
        }
        public bool ReachedPoint() => 
            Vector3.Distance(transform.position, _target.position) <= 0.5f;
        
        private void OnDeliveredStone()
        {
            _wallet.AddCoins(_currentPrice);
            _buildingController.SetCurrentPrice(_currentPrice);
        }

        private void OnChangedSpeed(float upgradeAmount) => 
            _currentSpeed += upgradeAmount;

        private void OnChangedIncome(int upgradeAmount) => 
            _currentPrice += upgradeAmount;
    }
}