using System.Collections;
using UnityEngine;

namespace CityBuilder.Building
{
    [RequireComponent(typeof(BuildingController))]
    public class Decoration : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _smokeEffect;
        [SerializeField] private GameObject _decoration;

        private const float Delay = 1f;

        private BuildingController _buildingController;

        private void Awake()
        {
            _buildingController = GetComponent<BuildingController>();
        }

        private void OnEnable()
        {
            _buildingController.ConstructedBuilding += OnConstructedBuildingController;
        }

        private void OnDisable()
        {
            _buildingController.ConstructedBuilding -= OnConstructedBuildingController;
        }

        private void OnConstructedBuildingController()
        {
            StartCoroutine(PlayEffect());
        }

        private IEnumerator PlayEffect()
        {
            _smokeEffect.Play();
            yield return new WaitForSeconds(Delay);
            _decoration.gameObject.SetActive(true);
        }
    }
}