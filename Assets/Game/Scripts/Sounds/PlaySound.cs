using UnityEngine;

namespace Scripts.Sounds
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        public void Play()
        {
            SoundManager.Instance.PlaySoundEffect(_clip);
        }
    }
}