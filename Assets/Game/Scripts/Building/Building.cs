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
        if (PlayerPrefs.HasKey("CurrentIndex"))
            _currentIndex = PlayerPrefs.GetInt("CurrentIndex", _currentIndex);

        for (int i = 0; i < _currentIndex; i++)
        {
            _buildingsParts[i].gameObject.SetActive(true);
        }
        
        _createdCanvasCoins = Instantiate(_canvasCoins, transform);

        foreach (var part in _buildingsParts)
        {
            part.Construct(_partSettings, _createdCanvasCoins, _createdCanvasCoins.GetComponent<CanvasGroup>(), 
                _createdCanvasCoins.GetComponent<ViewCoins>(), _particleSystem);
        }
    }
    
    public void GetStone()
    {
        if (_currentIndex == _buildingsParts.Length) return;

        _currentCountStones++;
        DeliveredStone?.Invoke();
        
        if (_currentCountStones == _stonesNeededNumber)
        {
            _buildingsParts[_currentIndex].Active();
            _currentIndex++;
            _currentCountStones = 0;
        }
        
        PlayerPrefs.SetInt("CurrentIndex", _currentIndex);
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