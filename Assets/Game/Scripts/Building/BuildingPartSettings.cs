using UnityEngine;

[System.Serializable]
public class BuildingPartSettings
{
    [SerializeField] private float _duration = 10f;
    [SerializeField] private float _offsetPosition = 3f;
    
    public float Duration => _duration;
    public float OffsetPosition => _offsetPosition;
}