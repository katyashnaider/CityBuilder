using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoneStorage : MonoBehaviour
{
    [SerializeField] private Stone _stones;
    [SerializeField] private StonePool _pool;
    [SerializeField] private float _movementDuration = 3f;
    [SerializeField] private int _maxStones = 3;
    [SerializeField] private int _stoneSpawnTime = 5;

    private List<Stone> _listStones;
    private float _elapsedTime = 0;
    private Coroutine _coroutine;

    private void Awake()
    {
        int initialStoneCount = 3;
        _listStones = new List<Stone>();
        
        for (int i = 0; i < initialStoneCount; i++)
            _listStones.Add(new Stone());
        
        SpawnOfStone();
        Debug.Log(_listStones.Count);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _stoneSpawnTime && _listStones.Count != _maxStones)
        {
            SpawnOfStone();
            _elapsedTime = 0;

            if (_listStones.Count == _maxStones)
                StopCoroutine(_coroutine);
        }
    }

    public void RemoveStone()
    {
        if (_listStones.Count > 0)
            _listStones.RemoveAt(0);
        else
            Debug.LogError("No stones left in the storage!");
    }

    private void SpawnOfStone()
    {
        Stone newStone = _pool.Get();

        _coroutine = StartCoroutine(MoveStone(newStone.transform, _movementDuration));
        _listStones.Add(newStone);
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