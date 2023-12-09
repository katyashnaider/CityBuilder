using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CityBuilder.UI
{
    internal sealed class Loading : MonoBehaviour
    {
        [SerializeField] private GameObject _loadScreen;
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private Slider _scale;
        [SerializeField] private float _delay = 1f;
        
        private const float CompletionThreshold = 0.9f;

        public void LoadScene(int levelNumber)
        {
            _loadScreen.gameObject.SetActive(true);
            _mainMenu.gameObject.SetActive(false);

            StartCoroutine(LoadAsync(levelNumber));
        }

        private IEnumerator LoadAsync(int levelNumber)
        {
            WaitForSeconds time = new WaitForSeconds(_delay);
            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(levelNumber);

            loadAsync.allowSceneActivation = false;

            while (!loadAsync.isDone)
            {
                _scale.value = Mathf.Clamp01(loadAsync.progress / CompletionThreshold);

                if (loadAsync.progress >= CompletionThreshold && !loadAsync.allowSceneActivation)
                {
                    yield return time;
                    loadAsync.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}