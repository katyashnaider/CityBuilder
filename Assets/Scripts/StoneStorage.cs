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
        int initialStoneCount = 5;
        _listStones = new List<Stone>();

        for (int i = 0; i < initialStoneCount; i++)
        {
            Stone stone = _pool.Get();
            float stonePositionX = 0;
            stone.transform.position = new Vector3(stonePositionX, stone.transform.position.y, stone.transform.position.z);
            stonePositionX += 0.5f;
            _listStones.Add(stone);
        }
        
        SpawnOfStone();
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
        
        Debug.Log(_listStones.Count);
    }

    public void RemoveStone()
    {
        if (_listStones.Count > 0)
        {
            Stone removedStone = _listStones[0];
            _pool.Put(removedStone);

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