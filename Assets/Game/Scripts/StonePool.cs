using UnityEngine;

public class StonePool : ObjectPool<Stone>
{
    [SerializeField] private Stone[] _stones;
    
    protected override Stone CreateObject()
    {
        for (int i = 0; i < _stones.Length; i++)
        {
            return Instantiate(_stones[i], transform);
        }

        return null;
    }
}