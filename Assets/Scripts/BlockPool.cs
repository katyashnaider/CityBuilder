using UnityEngine;

public class BlockPool : ObjectPool<Block>
{
    [SerializeField] private Block _block;

    protected override Block CreateObject()
    {
        return Instantiate(_block, transform);
    }
}