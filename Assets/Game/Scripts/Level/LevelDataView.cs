using System.Collections;
using CityBuilder.Sounds;
using CityBuilder.Worker;
using UnityEngine;

namespace CityBuilder.Level
{
    public sealed class LevelDataView
    {
        private const float AnimationDelay = 5f;
        private const float ActionDelay = 0.4f;
        private readonly GameObject _buttons;
        private readonly FactoryWorker _factoryWorker;
        private readonly GameObject _levelCompletedScreen;

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