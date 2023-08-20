using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingPartSettings _partSettings;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private BuildingPart[] _buildingsParts;
    [SerializeField] private Transform _canvasCoins;
    [SerializeField] private int _stonesNeededNumber = 5;

    private readonly List<Stone> _stones = new List<Stone>();

    private int _currentCountStones = 0;
    private int _currentIndex = 0;
    private Transform _currentStoneTransform;
    private Transform _createdCanvasCoins;
    private int _currentPrice;

    public event Action DeliveredStone;

    private void Awake()
    {
        _createdCanvasCoins = Instantiate(_canvasCoins, transform);

        foreach (var part in _buildingsParts)
        {
            //part.InActive();
            part.Construct(_partSettings, _createdCanvasCoins, _createdCanvasCoins.GetComponent<CanvasGroup>(), 
                _createdCanvasCoins.GetComponent<ViewCoins>(), _particleSystem);
        }
    }

    private void Update()
    {
        if (_stones.Count >= _stonesNeededNumber && _currentCountStones <= _stones.Count)
        {
           // _currentStoneTransform = _buildingsParts[_currentIndex].transform;
           // _buildingsParts[_currentIndex].gameObject.SetActive(true);
           
           _buildingsParts[_currentIndex].Active();
            _currentIndex++;
            _currentCountStones = 0;
            _stones.Clear();
        }
    }

    public void GetStone()
    {
        _stones.Add(new Stone());
        _currentCountStones++;
        DeliveredStone?.Invoke();
    }

    public Transform GetCurrentPosition() => _currentStoneTransform;

    public void SetCurrentPrice(int price)
    {
        foreach (var part in _buildingsParts)
        {
            part.InjectPrice(price);
        }
    }
}