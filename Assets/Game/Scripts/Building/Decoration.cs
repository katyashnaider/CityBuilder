using System.Collections;
using UnityEngine;

namespace Scripts.Building
{
    public class Decoration : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _smokeEffect;
        [SerializeField] private BuildingController _buildingController;
        [SerializeField] private GameObject _decoration;
    
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