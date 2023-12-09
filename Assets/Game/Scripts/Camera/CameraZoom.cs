using UnityEngine;

namespace CityBuilder.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float _portraitFOV = 30f;
        [SerializeField] private float _landscapeFOV = 20f;

        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
        }

        private void Start()
        {
            SetCameraFOV();
        }

        private void SetCameraFOV()
        {
            bool isLandscape = Screen.width > Screen.height;

            _camera.fieldOfView = isLandscape ? _landscapeFOV : _portraitFOV;
        }
    }
}