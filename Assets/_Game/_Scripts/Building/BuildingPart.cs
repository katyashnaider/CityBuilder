using System.Collections;
using CityBuilder.Sounds;
using CityBuilder.UI;
using DG.Tweening;
using UnityEngine;

namespace CityBuilder.Building
{
    public class BuildingPart : MonoBehaviour
    {
        private const float AnimationDelay = 2f;

        private CanvasGroup _canvasGroup;
        private AudioClip _clip;

        private Coroutine _coroutine;

        private Transform _createdCanvasCoinsRoot;
        private ParticleSystem _particleSystem;
        private ParticleSystem _particleSystemInstance;

        private int _price;
        private BuildingPartSettings _settings;
        private ViewCoins _viewCoins;
        private Tweener _moveTextTween;

        public void Construct(BuildingPartSettings settings, Transform createdCanvasCoins, CanvasGroup canvasGroup,
            ViewCoins viewCoins,
            ParticleSystem particleSystem, AudioClip clip)
        {
            _settings = settings;
            _particleSystem = particleSystem;
            _viewCoins = viewCoins;
            _canvasGroup = canvasGroup;
            _canvasGroup.alpha = 0;
            _createdCanvasCoinsRoot = createdCanvasCoins;
            _clip = clip;
        }

        public void InjectPrice(int price)
        {
            _price = price;
        }

        public void Active()
        {
            gameObject.SetActive(true);

            PlayParticleSystemEffect();

            // if (_coroutine is not null)
            // {
            //     StopCoroutine(_coroutine);
            // }

            _coroutine = StartCoroutine(LaunchAnimationParts());
        }

        private void PlayParticleSystemEffect()
        {
            Vector3 position = transform.position;

            _particleSystemInstance = Instantiate(_particleSystem, position, Quaternion.identity);
            _particleSystemInstance.transform.SetParent(transform);
            _particleSystemInstance.transform.position = new Vector3(position.x, position.y + 2f, position.z);

            _particleSystemInstance.Play();
            SoundManager.Instance.PlaySoundEffect(_clip);
        }

        private void OffAnimation()
        {
            _viewCoins.StopAnimation();

            _createdCanvasCoinsRoot.gameObject.SetActive(false);
            _canvasGroup.alpha = 1;

            if (_particleSystemInstance is not null)
            {
                _particleSystemInstance.Stop();
                //Destroy(_particleSystemInstance.gameObject);
            }
        }

        private IEnumerator LaunchAnimationParts()
        {
            OffAnimation();
            _particleSystemInstance.Play();

            WaitForSeconds launchAnimationParts = new WaitForSeconds(AnimationDelay);

            _viewCoins.UpdatePrice(_price);
            _createdCanvasCoinsRoot.gameObject.SetActive(true);
            Vector3 position = transform.position;

            _createdCanvasCoinsRoot.transform.SetParent(transform);
            _createdCanvasCoinsRoot.transform.position = new Vector3(position.x, position.y + 2f, position.z);
            _moveTextTween = _viewCoins.CycleText(_canvasGroup, _settings).OnComplete(OffAnimation);

            yield return launchAnimationParts;
        }
    }
}