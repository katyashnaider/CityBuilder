using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Camera
{
    internal sealed class CameraController : MonoBehaviour
    {
        private CinemachineFreeLook _freeLookCamera;

        private void Awake()
        {
            _freeLookCamera = GetComponent<CinemachineFreeLook>();
            _freeLookCamera.enabled = false;
        }

        private void Update() => _freeLookCamera.enabled = Input.GetMouseButton(0);
    }
}