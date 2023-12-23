using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.UI
{
    public class ShineAnimations : MonoBehaviour
    {
        [SerializeField] private Image _radialShine;
        [SerializeField] private float _rotationDuration;

        private void Start()
        {
            RotateInCircle();
        }

        private void RotateInCircle()
        {
            _radialShine.transform.eulerAngles = new Vector3(0, 0, 0);
            _radialShine.transform.DORotate(new Vector3(0, 0, -360), _rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
    }
}