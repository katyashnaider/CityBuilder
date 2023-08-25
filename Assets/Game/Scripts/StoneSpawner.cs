using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private StonePool _pool;
    [SerializeField] private Transform[] _positionsStones;

    private Animator _animator;
    private int _countStoneSpawn;
    private int _activeStone;
    private bool _isAnimationPlaying = true;

    private readonly List<Stone> _stones = new List<Stone>();

    private void Awake()
    {
        _countStoneSpawn = _positionsStones.Length;

        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        for (int i = 0; i < _countStoneSpawn; i++)
        {
            Stone stone = _pool.Get();
            stone.transform.position = _positionsStones[i].position;
            _stones.Add(stone);
        }

        for (int i = 0; i < _stones.Count / 2; i++)
        {
            _pool.Put(_stones[i]);
        }
    }

    private void Update()
    {
        _animator.SetBool(HashAnimator.IsRollingOver, _isAnimationPlaying);
    }

    public void OnIncludedStone()
    {
        Stone notActiveStone = _stones.FirstOrDefault(stone => stone.gameObject.activeSelf == false);
        _isAnimationPlaying = false;

        if (_activeStone <= _countStoneSpawn && notActiveStone != null)
        {
            notActiveStone.gameObject.SetActive(true);
            UpdateActiveStoneCount();

            if (_activeStone == _countStoneSpawn)
                return;

            _isAnimationPlaying = true;
        }
    }

    public void InActiveStone()
    {
        UpdateActiveStoneCount();

        if (_stones.Count > 0 && _activeStone > 0)
        {
            Stone activeStone = _stones.FirstOrDefault(stone => stone.gameObject.activeSelf);
            _pool.Put(activeStone);

            _isAnimationPlaying = true;
        }
        else
        {
            Debug.LogError("No stones left in the storage!");
        }
    }

    private int UpdateActiveStoneCount() => _activeStone = _stones.Count(stone => stone.gameObject.activeSelf);
}