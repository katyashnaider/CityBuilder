using UnityEngine;

namespace CityBuilder
{
    public class GpuInctancingEnabler : MonoBehaviour
    {
        private void Awake()
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
