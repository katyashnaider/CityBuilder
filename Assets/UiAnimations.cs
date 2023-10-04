using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Scripts.Ui
{
    public class UiAnimations : MonoBehaviour
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
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart); // Непрерывное вращение вокруг оси Z
        }
    }
}