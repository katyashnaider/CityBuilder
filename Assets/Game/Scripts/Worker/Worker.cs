using System.Collections;
using CityBuilder.Building;
using CityBuilder.Upgrade;
using JetBrains.Annotations;
using UnityEngine;

namespace CityBuilder.Worker
{
    [RequireComponent(typeof(Animator))]
    public class Worker : RestartEntity
    {
        [SerializeField] private BuildingPart _heldStone;

        private BuildingController _buildingController;

        private int _currentPath;
        private int _currentPrice;
        private IncomeUpgrade _incomeUpgrade;

        private Transform[] _pathPointsA;
        private Transform[] _pathPointsB;

        private SpeedUpgrade _speedUpgrade;

        private Transform _target;
        private Transform[] _targets;
        private Wallet.Wallet _wallet;

        private float _factorSpeed = 2;
        private float _durationModifySpeed = 3;
        private bool _isActiveModifySpeed = false;

        public Vector3 StartPosition { get; private set; }
        public float Speed { get; private set; }
        public StateMachine.StateMachine StateMachine { get; private set; }
        public Animator Animator { get; private set; }
        public bool IsStoneTaken { get; private set; }

        private void Update()
        {
            StateMachine.Tick();
        }

        private void OnDisable()
        {
            _buildingController.DeliveredStone -= OnDeliveredStone;
            _speedUpgrade.ChangedSpeed -= OnChangedSpeed;
            _incomeUpgrade.ChangedIncome -= OnChangedIncome;
        }

        public void Init(StateMachine.StateMachine stateMachine, SpeedUpgrade speedUpgrade, IncomeUpgrade incomeUpgrade, Transform[] targets, [CanBeNull] Transform[] pathPointsA, Transform[] pathPointsB, BuildingController buildingController, Wallet.Wallet wallet)
        {
            _speedUpgrade = speedUpgrade;
            _incomeUpgrade = incomeUpgrade;
            _targets = targets;
            _pathPointsA = pathPointsA;
            _pathPointsB = pathPointsB;
            _target = targets[0];
            _buildingController = buildingController;

            StateMachine = stateMachine;

            StartPosition = transform.position;

            Animator = GetComponent<Animator>();
            _wallet = wallet;

            _buildingController.DeliveredStone += OnDeliveredStone;
            _speedUpgrade.ChangedSpeed += OnChangedSpeed;
            _incomeUpgrade.ChangedIncome += OnChangedIncome;
        }

        public override void Restart()
        {
            _target = _targets[0];
            _currentPath = 0;
            IsStoneTaken = false;
        }

        public void SetStats(float speed, int price)
        {
            Speed = speed;
            _currentPrice = price;
        }

        public Vector3 GetTarget()
        {
            return _target.position;
        }

        public void SwitchTarget()
        {
            _currentPath++;

            if (_currentPath > 1)
            {
                _currentPath = 0;
            }

            _target = _currentPath % 2 == 0 ? _pathPointsA[Random.Range(0, _pathPointsA.Length)] : _pathPointsB[Random.Range(0, _pathPointsB.Length)];
        }

        public void TakeStone(bool isStoneVisible)
        {
            _heldStone.gameObject.SetActive(isStoneVisible);
            IsStoneTaken = true;
        }

        public void PutStone(bool isStoneVisible)
        {
            _heldStone.gameObject.SetActive(isStoneVisible);
            _buildingController.GetStone();
            IsStoneTaken = false;

        }
        public bool ReachedPoint()
        {
            return Vector3.Distance(transform.position, _target.position) <= 0.5f;
        }

        public void ApplySpeedModificator()
        {
            if (_isActiveModifySpeed)
            {
                return;
            }

            StartCoroutine(ModifySpeed(_factorSpeed, _durationModifySpeed));
        }

        private void OnDeliveredStone()
        {
            _wallet.AddCoins(_currentPrice);
            _buildingController.SetCurrentPrice(_currentPrice);
        }

        private void OnChangedSpeed(float upgradeAmount)
        {
            Speed += upgradeAmount;
        }

        private void OnChangedIncome(int upgradeAmount)
        {
            _currentPrice += upgradeAmount;
        }

        private IEnumerator ModifySpeed(float factor, float duration)
        {
            float originalSpeed = Speed;
            print("++");
            _isActiveModifySpeed = true;
            Speed *= factor;

            yield return new WaitForSeconds(duration);

            Speed = originalSpeed;
            _isActiveModifySpeed = false;
        }
    }
}