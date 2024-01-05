using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace CityBuilder.Camera
{
    internal sealed class CameraControl : MonoBehaviour
    {
        [SerializeField] private float _portraitFOV = 30f;
        [SerializeField] private float _landscapeFOV = 20f;
        [SerializeField] private float _zoomSpeed = 1.0f;
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

        private void Update()
        {
            float zoomInput = Input.GetAxis("Mouse ScrollWheel");

            if (Mathf.Abs(zoomInput) > 0.01f)
            {
                _freeLookCamera.enabled = true;
                AdjustZoom(zoomInput);
            }
        }

        private void AdjustZoom(float delta)
        {
            _freeLookCamera.m_Lens.FieldOfView += delta * _zoomSpeed;
            // Если вы хотите ограничить минимальное и максимальное значение зума, вы можете добавить следующие строки кода:
            // freeLookCamera.m_Lens.FieldOfView = Mathf.Clamp(freeLookCamera.m_Lens.FieldOfView, минимальное значение, максимальное значение);
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