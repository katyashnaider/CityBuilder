using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingPart[] _buildingsParts;
    [SerializeField] private int _stonesNeededNumber = 5;

    private List<Stone> _stones;
    private int _currentIndex = 0;

    private void Awake()
    {
        _stones = new List<Stone>();
    }

    private void Update()
    {
        if (_stones.Count >= _stonesNeededNumber && _currentIndex <= _stones.Count)
        {
            _buildingsParts[0].gameObject.SetActive(true);
            _currentIndex = 0;
            _stones.Clear();
        }
    }

    public void GetStone()
    {
        _stones.Add(new Stone());
        _currentIndex++;
        Debug.Log("количество камней " + _stones.Count);
    }
}