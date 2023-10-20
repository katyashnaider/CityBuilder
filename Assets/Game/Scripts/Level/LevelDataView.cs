using System.Collections;
using Scripts.Sounds;
using UnityEngine;
using Workers;

namespace Scripts.Level
{
    public class LevelDataView
    {
        private readonly GameObject _levelCompletedScreen;
        private readonly GameObject _buttons;
        private readonly FactoryWorker _factoryWorker;

        private const float AnimationDelay = 5f;
        private const float ActionDelay = 0.4f;
        
        public LevelDataView(GameObject levelCompletedScreen, GameObject buttons, FactoryWorker factoryWorker)
        {
            _levelCompletedScreen = levelCompletedScreen;
            _buttons = buttons;
            _factoryWorker = factoryWorker;
        }
        
        public IEnumerator ShowLevelCompletedScreen(bool isShown, AudioClip soundEffect)
        {
            _factoryWorker.gameObject.SetActive(!isShown);
            
            yield return new WaitForSeconds(ActionDelay);
            
            SoundManager.Instance.PlaySoundEffect(soundEffect);

            yield return new WaitForSeconds(AnimationDelay);

            _levelCompletedScreen.SetActive(isShown);
            _buttons.SetActive(!isShown);
        }
    }
}