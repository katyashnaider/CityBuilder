using UnityEngine;

namespace Scripts.Camera
{
    internal sealed class CameraRotation : MonoBehaviour
    {
        [SerializeField] private Transform _building;
        [SerializeField] private float _rotationSpeed = 1.0f;
        
        private bool _isBuildingNull;

        private void Start()
        {
            _isBuildingNull = _building == null;

            if (_isBuildingNull)
                Debug.LogWarning("BuildingController reference is not set for CameraRotationAroundBuilding script.");
        }
        
        private void Update()
        {
            if (_isBuildingNull)
                return;

            transform.RotateAround(_building.position, Vector3.up, _rotationSpeed * Time.deltaTime);
        }
    }
}