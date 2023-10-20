using Scripts.Building;
using JetBrains.Annotations;
using UnityEngine;
using Upgrades;
using Workers.StateMachines;

namespace Workers
{
    [RequireComponent(typeof(Animator))]
    public class Worker : MonoBehaviour
    {
        //создать класс Кошелек и там хранить деньги и проверять хватает ли денег +
        //создать класс Игрок и перенсти логику с Рабочего -
        //визуализировать то, когда камушки кладутся
        // сделать отдельно каждые колонки и стенки. создать класс который будет показывать количество монет (типо анимация через дотвин)+
        //переделать управление камерой (см класс камеры)
        [SerializeField] private Stone _heldStone;

        private SpeedUpgrade _speedUpgrade;
        private IncomeUpgrade _incomeUpgrade;
        private Transform[] _targets;
        private Transform[] _pathPointsA;
        private Transform[] _pathPointsB;
        private BuildingController _buildingController;
        // [SerializeField] private float _speed = 0.4f;
        //[SerializeField] private int _price = 5;

        private Transform _target;
        private StateMachine _stateMachine;
        private Wallet _wallet;
        private Animator _animator;

        private int _currentTargetIndex = 0;
        private int _currentPath = 0;

        //private int _wallet = 0;
        [SerializeField] private bool _isStoneTaken = false;
        private Vector3[] _pathTargets;
        private float _currentSpeed;
        private int _currentPrice;

        public Vector3 StartPosition { get; private set; }
        public float Speed => _currentSpeed;
        public StateMachine StateMachine => _stateMachine;
        public Animator Animator => _animator;
        public bool IsStoneTaken => _isStoneTaken;

        public void Init(StateMachine stateMachine, SpeedUpgrade speedUpgrade, IncomeUpgrade incomeUpgrade, Transform[] targets, [CanBeNull] Transform[] pathPointsA, Transform[] pathPointsB, BuildingController buildingController, Wallet wallet)
        {
            // Screen.SetResolution(1080, 1920, true);
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

            //ConvertTargetsToPath();
        }

        private void OnDisable()
        {
            _buildingController.DeliveredStone -= OnDeliveredStone;
            _speedUpgrade.ChangedSpeed -= OnChangedSpeed;
            _incomeUpgrade.ChangedIncome -= OnChangedIncome;
        }

        private void Update() => _stateMachine.Tick();

        public void GetTarget(Vector3[] pathTargets)
        {
            //Debug.Log("работает класс Worker метод GetTarget");
            _pathTargets = pathTargets;
        }

        public void SetStats(float speed, int price)
        {
            _currentSpeed = speed;
            _currentPrice = price;
        }

        public Vector3 GetTarget()
        {
            return _target.position;
        }

        public void SwitchTarget()
        {
            //Debug.Log("работает класс Worker метод SwitchTarget");
            //Debug.Log(_currentTargetIndex);
            _currentPath++;
            // _currentTargetIndex++;
            // if (_currentTargetIndex >= _targets.Length)
            //     ResetTargetIndex();
            if (_currentPath > 1)
                _currentPath = 0;

            if (_currentPath % 2 == 0)
                _target = _pathPointsA[Random.Range(0, _pathPointsA.Length)];
            else
                _target = _pathPointsB[Random.Range(0, _pathPointsB.Length)];
        }

        public void TakeStone(bool isStoneVisible) //виден ли камень
        {
            _heldStone.gameObject.SetActive(isStoneVisible);
            // _stoneStorage.RemoveStone();
            //_stoneSpawner.InActiveStone();
            _isStoneTaken = true;
        }

        public void PutStone(bool isStoneVisible) //виден ли камень
        {
            _heldStone.gameObject.SetActive(isStoneVisible);
            _buildingController.GetStone();
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
            _buildingController.SetCurrentPrice(_currentPrice);
        }

        private void OnChangedSpeed(float upgradeAmount)
        {
            _currentSpeed += upgradeAmount;
        }

        private void OnChangedIncome(int upgradeAmount)
        {
            _currentPrice += upgradeAmount;
        }

        private void ConvertTargetsToPath()
        {
            _pathTargets = new Vector3[_targets.Length];

            for (int i = 0; i < _targets.Length; i++)
            {
                _pathTargets[i] = _targets[i].position;
            }
        }

        public bool ReachedPoint()
        {
            //return ReachedPoint(targetPointA);
            //float distance = Vector3.Distance(transform.position, _targets[_currentTargetIndex].position);
            float distance = Vector3.Distance(transform.position, _target.position);

            return distance <= 0.5f;
        }
    }
}