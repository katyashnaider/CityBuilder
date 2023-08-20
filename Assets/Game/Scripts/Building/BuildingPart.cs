using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingPart : MonoBehaviour
{
    private BuildingPartSettings _settings;
    private Transform _createdCanvasCoins;
    private CanvasGroup _canvasGroup;
    private ViewCoins _viewCoins;
    private ParticleSystem _particleSystem;
    private ParticleSystem _particleSystemInstance;

    private int _price;

    public void Construct(BuildingPartSettings settings, Transform createdCanvasCoins, CanvasGroup canvasGroup, ViewCoins viewCoins,
        ParticleSystem particleSystem)
    {
        _settings = settings;
        _particleSystem = particleSystem;
        _viewCoins = viewCoins;
        _canvasGroup = canvasGroup;
        _createdCanvasCoins = createdCanvasCoins;
    }

    public void InActive() => gameObject.SetActive(false);

    public void InjectPrice(int price)
    {
        _price = price;
    }

    public void Active()
    {
        gameObject.SetActive(true);

        PlayParticleSystemEffect();
        StartCoroutine(LaunchAnimationParts());
    }
    private void PlayParticleSystemEffect()
    {
        Vector3 position = transform.position;

        _particleSystemInstance = Instantiate(_particleSystem, position, Quaternion.identity);
        _particleSystemInstance.transform.SetParent(transform);
        _particleSystemInstance.transform.position = new Vector3(position.x, position.y + 2f, position.z);

        _particleSystemInstance.Play();
    }

    private void SetPosition(Transform transformPart)
    {
        Vector3 position = transformPart.position;

        _createdCanvasCoins.transform.parent = transformPart;
        _createdCanvasCoins.transform.position = new Vector3(position.x, position.y + 2f, position.z);
    }

    private void CicleText(CanvasGroup canvas)
    {
        canvas.transform.DOMoveY(canvas.transform.position.y + _settings.OffsetPosition, _settings.Duration).SetEase(Ease.OutQuad);
        canvas.DOFade(0f, _settings.Duration).SetEase(Ease.Linear).OnComplete(OffAnimation);
    }

    private void OffAnimation()
    {
        _createdCanvasCoins.gameObject.SetActive(false);
        _canvasGroup.alpha = 1;

        if (_particleSystemInstance != null)
        {
            Destroy(_particleSystemInstance.gameObject);
        }

    }

    private IEnumerator LaunchAnimationParts()
    {
        WaitForSeconds launchAnimationParts = new WaitForSeconds(2f);

        _viewCoins.UpdatePrice(_price);
        //gameObject.SetActive(true);
        _createdCanvasCoins.gameObject.SetActive(true);

        SetPosition(transform);
        CicleText(_canvasGroup);
        yield return launchAnimationParts;
    }
}