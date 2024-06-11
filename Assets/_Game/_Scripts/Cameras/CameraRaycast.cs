using CityBuilder.Workers;
using UnityEngine;

namespace CityBuilder.Cameras
{
    public class CameraRaycast : MonoBehaviour
    {
        public static bool IsWorkerClicked { get; private set; } = false;
        
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
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
                        IsWorkerClicked = true; // Устанавливаем флаг при нажатии на рабочего
                    }
                    else
                    {
                        IsWorkerClicked = false; // Сбрасываем флаг если нажатие не на рабочего
                    }
                }
                else
                {
                    IsWorkerClicked = false; // Сбрасываем флаг если нажатие не попало никуда
                }
            }
        }
    }
}