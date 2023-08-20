using TMPro;
using UnityEngine;

namespace Game.Scripts.Pool
{
    public class TextPool : ObjectPool<TMP_Text>
    {
        [SerializeField] private TMP_Text _text;
        
        protected override TMP_Text CreateObject() => Instantiate(_text, transform);
    }
}