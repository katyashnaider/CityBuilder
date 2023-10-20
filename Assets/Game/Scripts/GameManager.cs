using UnityEngine;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        public void TogglePause(bool isEnabled) => Time.timeScale = isEnabled ? 0 : 1;
    }
}