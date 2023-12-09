using UnityEngine;

namespace CityBuilder.Sounds
{
    public class ToggleAudio : MonoBehaviour
    {
        public void Toggle()
        {
            SoundManager.Instance.ToggleMusic();
        }
    }
}