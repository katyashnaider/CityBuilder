using Pool;
using UnityEngine;
using Upgrades;
using Workers.StateMachines;
using Workers.StateMachines.States;

namespace Workers
{
    public class FactoryWorker : MonoBehaviour
    {
        [SerializeField] private WorkerPool _pool;
        [SerializeField] private SpeedUpgrade _speedUpgrade;
        [SerializeField] private IncomeUpgrade _incomeUpgrade;
        [SerializeField] private Transform[] _targets;
        [SerializeField] private StoneSpawner _stoneSpawner;
        [SerializeField] private Building _building;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private float _speed = 0.4f;
        [SerializeField] private int _price = 5;

        public Worker CreateWorker(Vector3 position, Quaternion quaternion)
        {
            var worker = _pool.Get();

            worker.transform.position = position;
            worker.transform.rotation = quaternion;

            worker.Init(new StateMachine(), _speedUpgrade, _incomeUpgrade, _targets, _stoneSpawner, _building, _wallet);

            worker.SetStats(_speed + _speedUpgrade.GetValue(), _price +_incomeUpgrade.GetValue());

            var walking = new Walking(worker, worker.Animator);
            var takingStone = new TakingStone(worker);
            var putsStone = new PutsStone(worker);
            
            worker.StateMachine.AddTransition(walking, takingStone, () => worker.ReachedPoint() && !worker.IsStoneTaken);
            worker.StateMachine.AddTransition(walking, putsStone, () => worker.ReachedPoint() && worker.IsStoneTaken);
            worker.StateMachine.AddTransition(takingStone, walking, () => worker.IsStoneTaken);
            worker.StateMachine.AddTransition(putsStone, walking, () => !worker.IsStoneTaken);
            
            return worker;
        }
    }
}