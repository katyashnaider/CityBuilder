using CityBuilder.Sounds;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CityBuilder.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public void OpenMenu(int numberScene)
        {
            SceneManager.LoadSceneAsync(numberScene);
            SoundManager.Instance.StopSoundGame();
            DOTween.KillAll();
        }
    }
}