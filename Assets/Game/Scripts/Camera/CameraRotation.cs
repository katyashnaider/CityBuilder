using UnityEngine;

namespace Scripts.Camera
{
    internal sealed class CameraRotation : MonoBehaviour
    {
        public Transform building; // Ссылка на объект здания, вокруг которого нужно вращать камеру
        public float rotationSpeed = 1.0f; // Скорость вращения камеры
        private bool _isbuildingNull;

        private void Start()
        {
            _isbuildingNull = building == null;

            if (_isbuildingNull)
                Debug.LogWarning("Building reference is not set for CameraRotationAroundBuilding script.");
        }
        
        void Update()
        {
            if (_isbuildingNull)
                return;

            transform.RotateAround(building.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}