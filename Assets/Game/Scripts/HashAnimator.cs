using UnityEngine;

public class HashAnimator : MonoBehaviour
{ 
    public static readonly int IsStoneTaken = Animator.StringToHash("isStoneTaken");
    public static readonly int IsRollingOver = Animator.StringToHash("IsRollingOver");
    public static readonly int Unloading = Animator.StringToHash("Unloading");
}