using UnityEngine;

namespace CityBuilder
{
    public class HashAnimator : MonoBehaviour
    {
        public static readonly int Unloading = Animator.StringToHash("Unloading");
        public static readonly int Enabled = Animator.StringToHash("Enabled");
    }
}