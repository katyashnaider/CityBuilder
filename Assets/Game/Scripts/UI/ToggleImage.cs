using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
    [SerializeField] private Sprite[] _toggleSprites; // Массив спрайтов для переключения
    
    private Image _imageComponent;
    private int _currentIndex = 0;

    private void Awake() => 
        _imageComponent = GetComponent<Image>();

    void Start()
    {
        if (_toggleSprites.Length > 0)
            _imageComponent.sprite = _toggleSprites[_currentIndex];
    }

    public void Toggle()
    {
        if (_toggleSprites.Length == 0)
            return;

        _currentIndex = (_currentIndex + 1) % _toggleSprites.Length;
        _imageComponent.sprite = _toggleSprites[_currentIndex];
    }
}
