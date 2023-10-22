using System.Collections;
using Cinemachine;
using DG.Tweening;
using Scripts.Sounds;
using UnityEngine;
using Workers;

namespace Scripts.Level
{
    internal sealed class LevelDataView
    {
        private readonly GameObject _levelCompletedScreen;
        private readonly GameObject _buttons;
        private readonly FactoryWorker _factoryWorker;

        private MonoBehaviour _component;

        private const float AnimationDelay = 5f;
        private const float ActionDelay = 0.4f;

        public LevelDataView(GameObject levelCompletedScreen, GameObject buttons, FactoryWorker factoryWorker)
        {
            _levelCompletedScreen = levelCompletedScreen;
            _buttons = buttons;
            _factoryWorker = factoryWorker;
        }

        // public void RotateCamera(Transform target, float rotationDuration, bool isShown, AudioClip soundEffect)
        // {
        //     Debug.Log("+++++++++");
        //     if (target == null) return;
        //     // isRotating = true;
        //     Debug.Log("-------");
        //
        //     Transform cameraTransform;
        //
        //     const float RotationAngle = 360.0f;
        //
        //     _camera.transform.DORotate(new Vector3((cameraTransform = _camera.transform).position.x, RotationAngle, cameraTransform.position.z),
        //             rotationDuration, RotateMode.FastBeyond360)
        //         .OnComplete(() => StartCoroutine(isShown, soundEffect));
        // }
        
        public void StartCoroutine(bool isShown, AudioClip soundEffect) =>
            _component.StartCoroutine(ShowLevelCompletedScreen(isShown, soundEffect));

        private IEnumerator ShowLevelCompletedScreen(bool isShown, AudioClip soundEffect)
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