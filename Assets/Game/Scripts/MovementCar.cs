using DG.Tweening;
using UnityEngine;

public class MovementCar : MonoBehaviour
{
    [SerializeField] private Transform[] _pathMove;
    [SerializeField] private float _duration = 10f;

    private int _currentIndex = 0;
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    private void Start() => StartUnloadAnimation();

    private void MoveToNextPoint()
    {
        if (_currentIndex < 0 || _currentIndex >= _pathMove.Length) return;

        Vector3[] pathPositions =
        {
            transform.position,
            _pathMove[_currentIndex].position
        };

        transform.DOPath(pathPositions, _duration).SetEase(Ease.Linear)
            .OnComplete(() => {
                if (_currentIndex == _pathMove.Length - 1)
                {
                    StartUnloadAnimation();
                    _currentIndex = 0;
                }
                else
                {
                    _currentIndex++;
                    MoveToNextPoint();
                }
            });
    }

    private void StartUnloadAnimation()
    {
        _animator.SetTrigger(HashAnimator.Unloading);

        float unloadAnimationDuration = _animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke(nameof(MoveToNextPoint), unloadAnimationDuration);
    }
}