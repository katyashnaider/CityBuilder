using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder
{
    public class DisableMeshRenderers : MonoBehaviour
    {
        [SerializeField] private Button _buttonDisable;

        private void OnEnable()
        {
            _buttonDisable.onClick.AddListener(Disable);
        }

        private void OnDisable()
        {
            _buttonDisable.onClick.RemoveListener(Disable);
        }

        private void Disable()
        {
            MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();

            foreach (MeshRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }
        }
    }
}