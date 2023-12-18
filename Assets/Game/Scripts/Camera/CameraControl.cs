using System.Collections;
using Cinemachine;
using UnityEngine;

namespace CityBuilder.Camera
{
    internal sealed class CameraControl : MonoBehaviour
    {
        [SerializeField] private float _portraitFOV = 30f;
        [SerializeField] private float _landscapeFOV = 20f;
        
        private const float Delay = 0.1f;

        private CinemachineFreeLook _freeLookCamera;

        private void Awake()
        {
            _freeLookCamera = GetComponent<CinemachineFreeLook>();
        }

        private void Start()
        {
            StartCoroutine(InputControl());
        }

        private void SetFOV()
        {
            bool isLandscape = Screen.width > Screen.height;
            _freeLookCamera.m_Lens.FieldOfView = isLandscape ? _landscapeFOV : _portraitFOV;
        }

        private IEnumerator InputControl()
        {
            SetFOV();

            yield return new WaitForSeconds(Delay);

            while (true)
            {
                _freeLookCamera.enabled = Input.GetMouseButton(0);
                yield return null;
            }
        }
    }
}