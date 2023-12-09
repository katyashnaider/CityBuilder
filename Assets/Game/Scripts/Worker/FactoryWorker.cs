using CityBuilder.Building;
using CityBuilder.Pool;
using CityBuilder.Upgrade;
using CityBuilder.Worker.StateMachine.States;
using UnityEngine;

namespace CityBuilder.Worker
{
    public class FactoryWorker : MonoBehaviour
    {
        [SerializeField] private WorkerPool _pool;
        [SerializeField] private SpeedUpgrade _speedUpgrade;
        [SerializeField] private IncomeUpgrade _incomeUpgrade;
        [SerializeField] private Transform[] _targets;
        [SerializeField] private Transform[] _pathPointsA;
        [SerializeField] private Transform[] _pathPointsB;
        [SerializeField] private BuildingController _buildingController;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private float _speed = 0.4f;
        [SerializeField] private int _price = 5;
        
        private int _currentPrice;
        private float _currentSpeed;

        private void Awake()
        {
            _currentSpeed = _speed;
            _currentPrice = _price;
        }

        public Worker CreateWorker(Vector3 position, Quaternion quaternion)
        {
            Worker worker = _pool.Get();

            worker.transform.position = position;
            worker.transform.rotation = quaternion;

            worker.Init(new StateMachine.StateMachine(), _speedUpgrade, _incomeUpgrade, _targets, _pathPointsA, _pathPointsB, _buildingController, _wallet);

            worker.SetStats(_currentSpeed + _speedUpgrade.GetValue(), _currentPrice + _incomeUpgrade.GetValue());

            Walking walking = new Walking(worker);
            TakingStone takingStone = new TakingStone(worker);
            PutsStone putsStone = new PutsStone(worker);

            worker.StateMachine.AddTransition(walking, takingStone, () => worker.ReachedPoint() && !worker.IsStoneTaken);
            worker.StateMachine.AddTransition(walking, putsStone, () => worker.ReachedPoint() && worker.IsStoneTaken);
            worker.StateMachine.AddTransition(takingStone, walking, () => worker.IsStoneTaken);
            worker.StateMachine.AddTransition(putsStone, walking, () => !worker.IsStoneTaken);

            return worker;
        }
    }
}