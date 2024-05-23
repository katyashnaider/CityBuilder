using System;
using System.Collections;
using Cinemachine;
using CityBuilder.Workers;
using DG.Tweening;
using UnityEngine;

namespace CityBuilder
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _popup1;
        [SerializeField] private CanvasGroup _popup2;
        [SerializeField] private Vector3 _targetScale = new(2f, 2f, 2f);
        [SerializeField] private float _offsetPosition = 2f;
        [SerializeField] private float _durationOffset = 1f;
        [SerializeField] private float _durationScale = 0.5f;
        [SerializeField] private float _secondsEnd = 3f;

        private Camera _camera;
        private Tween _scaleTween;
        private bool _isCompletedTutorial = false;
        private Quaternion _lastCameraRotation;
        private Vector2 _startPosition;
        private Coroutine _coroutine;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _isCompletedTutorial = PlayerPrefs.GetInt("IsCompletedTutorial", 0) == 1;
            gameObject.SetActive(!_isCompletedTutorial);

            if (_isCompletedTutorial == false)
            {
                StartScaleAnimation(_popup1);
            }
        }

        private void Update()
        {
            if (_isCompletedTutorial)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.TryGetComponent(out Worker _))
                    {
                        StartAnimation(_popup1, RunCoroutine);
                    }
                }
            }
        }

        private void StartAnimation(CanvasGroup popup, Action onCompleteAction)
        {
            _scaleTween.Kill();

            popup.gameObject.transform
                .DOMoveY(popup.transform.position.y + _offsetPosition, _durationOffset)
                .SetEase(Ease.OutQuad);
            popup.DOFade(0f, _durationOffset).SetEase(Ease.Linear)
                .OnComplete(() => onCompleteAction());
        }

        private void StartScaleAnimation(CanvasGroup popup)
        {
            popup.transform.localScale = Vector3.one;

            _scaleTween = popup.transform.DOScale(_targetScale, _durationScale)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    popup.transform.DOScale(Vector3.one, _durationScale)
                        .SetEase(Ease.OutQuad)
                        .OnComplete(() => StartScaleAnimation(popup));
                });
        }

        private void RunCoroutine()
        {
            _coroutine = StartCoroutine(ShowPopup());
        }

        private void CompleteTutorial()
        {
            _isCompletedTutorial = true;
            StopCoroutine(_coroutine);
            gameObject.SetActive(false);

            PlayerPrefs.SetInt("IsCompletedTutorial", _isCompletedTutorial ? 1 : 0);
            PlayerPrefs.Save();
        }

        private IEnumerator ShowPopup()
        {
            _popup2.alpha = 1;
            StartScaleAnimation(_popup2);

            yield return new WaitForSeconds(_secondsEnd);

            StartAnimation(_popup2, CompleteTutorial);
        }
    }
}