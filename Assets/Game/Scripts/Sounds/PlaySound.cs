using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Sounds
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        private void Start() => 
            SoundManager.Instance.PlaySound(_clip);
    }
}