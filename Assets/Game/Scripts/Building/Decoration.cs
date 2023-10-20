using System.Collections;
using UnityEngine;

namespace Scripts.Building
{
    [RequireComponent(typeof(BuildingController))]
    public class Decoration : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _smokeEffect;
        [SerializeField] private GameObject _decoration;

        private BuildingController _buildingController;
        
        private void Awake() => 
            _buildingController = GetComponent<BuildingController>();

        private const float Delay = 1f;

        private void OnEnable() => 
            _buildingController.ConstructedBuilding += OnConstructedBuildingController;

        private void OnDisable() => 
            _buildingController.ConstructedBuilding -= OnConstructedBuildingController;

        private void OnConstructedBuildingController() => 
            StartCoroutine(PlayEffect());

        private IEnumerator PlayEffect()
        {
            _smokeEffect.Play();
            yield return new WaitForSeconds(Delay);
            _decoration.gameObject.SetActive(true);
        }
    }
}