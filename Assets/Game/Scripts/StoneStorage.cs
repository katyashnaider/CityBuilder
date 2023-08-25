using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

//сделать массив камней и рандом чтобы они включались в руках у гнома
public class StoneStorage : MonoBehaviour
{
    [SerializeField] private Stone _stones;
    [SerializeField] private StonePool _pool;
    [SerializeField] private float _movementDuration = 3f;
    [SerializeField] private float _rotateDuration = 3f;
    [SerializeField] private int _maxStones = 3;
    [SerializeField] private int _stoneSpawnTime = 5;

    [SerializeField] private Transform _target;
    [SerializeField] private float rotationDuration = 30f;
    [SerializeField] private float _errorDistance = 3f;

    private bool _isReachedTarget = false;
    private Tween _rotationTween;
    private int _currentStoneIndex = 0;
    private bool _isMoving = false;

    private List<Stone> _listStones;
    private float _elapsedTime = 0;
    private Coroutine _coroutine;

    private void Awake()
    {
        int initialStoneCount = 5;
        _listStones = new List<Stone>();

        //_coroutine = StartCoroutine(SpawnOfStones());
        //Stone stone = _pool.Get();
        //float stonePositionX = 0;
        //stone.transform.position = new Vector3(stonePositionX, stone.transform.position.y, stone.transform.position.z);
        //stonePositionX += 0.5f;
        //_listStones.Add(stone);

        //if (_listStones.Count >= initialStoneCount)
        // StopCoroutine(_coroutine);
        //_coroutine = StartCoroutine(SpawnOfStones());

        for (int i = 0; i < _maxStones; i++)
        {
            SpawnOfStone();
        }
    }

    private void Update()
    {
        if (_isMoving == false && _listStones.Count > 0)
        {
            StartCoroutine(MoveStoneToTarget());
        }

        //if (_listStones.Count == _maxStones)
        //StopCoroutine(_coroutine);
        //}

        /* if (_listStones.Count != _maxStones)
         {
             Debug.Log("старт корутина");
             _coroutine = StartCoroutine(SpawnOfStones());
         }
         else*/
        /* if (_listStones.Count == _maxStones)
        {
            Debug.Log("стоп корутина");
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        if (_listStones.Count == 0)
        {
            _coroutine = StartCoroutine(SpawnOfStones());
        }

        Debug.Log(_listStones.Count);
        */
    }

    public void RemoveStone()
    {
        Vector3 currentPosition = transform.position;

        if (_listStones.Count > 0)
        {
            Stone removedStone = _listStones[0];
            _pool.Put(removedStone);
            removedStone.transform.position = currentPosition;

            _listStones.RemoveAt(0);
        }
        else
        {
            Debug.LogError("No stones left in the storage!");
        }
    }

    private void SpawnOfStone()
    {
        Stone newStone = _pool.Get();
        _listStones.Add(newStone);
    }

    private IEnumerator MoveStoneToTarget()
    {
        _isMoving = true;

        Stone stone = _listStones[_currentStoneIndex];

        Quaternion rotationStone = stone.transform.rotation;

        if (_rotationTween == null)
            _rotationTween = stone.transform.DORotate(new Vector3(rotationStone.x, rotationStone.y, 360f),
                    rotationDuration, RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetEase(Ease.Linear);

        stone.transform.DOMove(_target.position, _movementDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => OnMovementComplete(stone));

        yield return new WaitForSeconds(_stoneSpawnTime);

        _isMoving = false;

        _currentStoneIndex++;

        if (_currentStoneIndex >= _listStones.Count)
        {
            _currentStoneIndex = 0;
        }
    }

    private void OnMovementComplete(Stone stone)
    {
        _rotationTween.Kill();
        _rotationTween = null;
        //_isReachedTarget = true;
        // StopRotation();
        //Debug.Log("Цель достигнута. Вращение остановлено.");
    }

    private void OnRotationKill()
    {
        _isReachedTarget = false;
        _rotationTween.Kill();
    }

    private void StopRotation()
    {
        if (_rotationTween != null)
        {
            Debug.Log("StopRotation");
            _rotationTween.Kill();
            _rotationTween = null;
        }
    }

    private IEnumerator SpawnOfStones()
    {
        while (true)
        {
            if (_listStones.Count != _maxStones)
            {
                Debug.Log("спавн камня");
                SpawnOfStone();
            }

            yield return new WaitForSeconds(_stoneSpawnTime);
        }
    }

    private IEnumerator MoveStone(Transform stonePosition, float movementDuration)
    {
        //попробовать поменять на передвжиение через дотвин
        float delay = 0.5f;
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0;
        Vector3 startPosition = stonePosition.position;
        Vector3 targetPosition = new Vector3(startPosition.x - 0.5f, startPosition.y, startPosition.z);

        while (elapsedTime <= movementDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / movementDuration);
            Vector3 newPosition = Vector3.MoveTowards(stonePosition.position, targetPosition, normalizedTime);
            stonePosition.position = newPosition;
        }
    }
}