using UnityEngine;
using UnityEngine.Serialization;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 3.0f;
        [SerializeField] private float _maxHorizontalAngle = 80.0f;
        [SerializeField] private float _minHorizontalAngle = -80.0f;

        private bool _isRotating = false;
        private float _mouseX;

        private void Update()
        {
            HandleRotationInput();
        }

        private void HandleRotationInput() //обработка ввода поворота
        {
            if (Input.GetMouseButtonDown(0))
                SetRotationState(true, CursorLockMode.Locked, false);

            if (Input.GetMouseButtonUp(0))
                SetRotationState(false, CursorLockMode.None, true);

            if (_isRotating)
                RotateHorizontally();
        }

        private void RotateHorizontally()
        {
            Vector3 eulerAngles = transform.eulerAngles;

            _mouseX -= Input.GetAxis("Mouse X") * _rotationSpeed;
            _mouseX = Mathf.Clamp(_mouseX, _minHorizontalAngle, _maxHorizontalAngle);

            eulerAngles = new Vector3(eulerAngles.x, _mouseX, eulerAngles.z);
            transform.eulerAngles = eulerAngles;
        }

        private void SetRotationState(bool isRotating, CursorLockMode lockMode, bool cursorVisible)
        {
            _isRotating = isRotating;
            Cursor.lockState = lockMode;
            Cursor.visible = cursorVisible;
        }
    }
}