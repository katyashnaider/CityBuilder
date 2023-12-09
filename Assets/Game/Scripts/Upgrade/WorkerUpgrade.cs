using CityBuilder.Worker;
using CityBuilder.Worker.StateMachine.States;
using UnityEngine;

namespace CityBuilder.Upgrade
{
    public class WorkerUpgrade : Upgrade
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private FactoryWorker _factoryWorker;
        [SerializeField] private float _offsetPosition = 5f;

        private void Start()
        {
            if (PlayerPrefs.HasKey("AddWorkerUpgrade"))
            {
                LoadProgress("AddWorkerUpgrade");
            }

            for (int i = 0; i < CurrentLevel; i++)
            {
                Vector3 position = _spawnPoint.position;
                position.z = Random.Range(position.z - _offsetPosition, position.z + _offsetPosition);

                Worker.Worker worker = _factoryWorker.CreateWorker(position, _spawnPoint.rotation);
                worker.StateMachine.SetState<Walking>();
            }

            UpgradeInfo();
        }

        public override void ApplyUpgrade()
        {
            if (_wallet.HasEnoughCoins(CurrentPrice))
            {
                base.ApplyUpgrade();
                Worker.Worker worker = _factoryWorker.CreateWorker(_spawnPoint.position, _spawnPoint.rotation);

                worker.StateMachine.SetState<Walking>();
                SaveProgress("AddWorkerUpgrade");
            }
        }
    }
}