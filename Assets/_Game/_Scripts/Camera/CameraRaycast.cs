using Cinemachine;
using CityBuilder.Workers;
using UnityEngine;

namespace CityBuilder.Camera
{
    public class CameraRaycast : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        
        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent(out Worker worker))
                    {
                        worker.ApplySpeedModificator();
                    }
                }
            }
        }
    }
}