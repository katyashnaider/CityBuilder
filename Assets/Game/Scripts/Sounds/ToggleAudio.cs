using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Sounds
{
    public class ToggleAudio : MonoBehaviour
    {
        public void Toggle() => SoundManager.Instance.ToggleMusic();
    }
}