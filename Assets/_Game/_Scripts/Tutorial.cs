using System;
using System.Collections;
using CityBuilder.Workers;
using DG.Tweening;
using UnityEngine;

namespace CityBuilder
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _popup;
        [SerializeField] private Vector3 _targetScale = new(2f,2f, 2f);
        [SerializeField] private float _offsetPosition = 2f;
        [SerializeField] private float _durationOffset = 1f;
        [SerializeField] private float _durationScale = 0.5f;

        private Camera _camera;
        private Tween _scaleTween;
        private bool _isCompletedTutorial = false;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _isCompletedTutorial = PlayerPrefs.GetInt("IsCompletedTutorial", 0) == 1;
            gameObject.SetActive(!_isCompletedTutorial);
            
            StartScaleAnimation();
        }
        
        private void StartScaleAnimation()
        {
            _popup.transform.localScale = Vector3.one;
            
            _scaleTween = _popup.transform.DOScale(_targetScale, _durationScale)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _popup.transform.DOScale(Vector3.one, _durationScale)
                        .SetEase(Ease.OutQuad)
                        .OnComplete(StartScaleAnimation);
                });
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.TryGetComponent(out Worker _))
                    {
                        _scaleTween.Kill();
                        
                        _popup.gameObject.transform.DOMoveY(_popup.transform.position.y + _offsetPosition, _durationOffset)
                            .SetEase(Ease.OutQuad);
                        _popup.DOFade(0f, _durationOffset).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));

                        _isCompletedTutorial = true;
                        
                        PlayerPrefs.SetInt("IsCompletedTutorial", _isCompletedTutorial ? 1 : 0);
                        PlayerPrefs.Save();
                    }
                }
            }
        }

    }
}