using UnityEngine;
using UnityEngine.Serialization;

public class StonePool : ObjectPool<Stone>
{
    [SerializeField] private Stone _stone;

    protected override Stone CreateObject()
    {
        return Instantiate(_stone, transform);
    }
}