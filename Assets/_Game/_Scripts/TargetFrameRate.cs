using UnityEngine;

namespace CityBuilder
{
    public class TargetFrameRate : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}