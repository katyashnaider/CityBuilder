using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Camera
{
    internal sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private float _portraitFOV = 30f;
        [SerializeField] private float _landscapeFOV = 20f;
        
        private CinemachineFreeLook _freeLookCamera;
        private const float Delay = 0.1f;
        
        private void Awake() => 
            _freeLookCamera = GetComponent<CinemachineFreeLook>();

        private void Start() =>
            StartCoroutine(CameraInputControl());

        private void SetCameraFOV()
        {
            bool isLandscape = Screen.width > Screen.height;
            _freeLookCamera.m_Lens.FieldOfView = isLandscape ? _landscapeFOV : _portraitFOV;
        }

        private IEnumerator CameraInputControl()
        {
            SetCameraFOV();

            yield return new WaitForSeconds(Delay);

            while (true)
            {
                _freeLookCamera.enabled = Input.GetMouseButton(0);
                yield return null;
            }
        }
    }
}