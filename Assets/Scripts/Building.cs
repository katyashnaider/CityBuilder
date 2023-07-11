using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingPart[] _buildingsParts;
    [SerializeField] private int _stonesNeededNumber = 5;

    private List<Stone> _stones;
    private int _currentCountStones = 0;
    private int _currentIndex = 0;

    public event Action DeliveredStone;

    private void Awake()
    {
        _stones = new List<Stone>();
    }

    private void Update()
    {
        if (_stones.Count >= _stonesNeededNumber && _currentCountStones <= _stones.Count)
        {
            _buildingsParts[_currentIndex].gameObject.SetActive(true);
            
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
}