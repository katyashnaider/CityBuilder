using CityBuilder.Building;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CityBuilder.UI
{
    public class ViewCoins : RestartEntity
    {
        [SerializeField] private TMP_Text _priceText;

        private Tweener _moveAnimation;
        private Tweener _fadeOutAnimation;
        
        public void UpdatePrice(int price)
        {
            Debug.Log("До " + price + _priceText.transform.GetFullPath(), _priceText.transform.parent);
            _priceText.text = price.ToString();
            Debug.Log("После " + price);
            Debug.Log("Текст " + _priceText.text);
        }
        
        public void SetPosition(Transform transformPart, Transform createdCanvasCoins)
        {
            Vector3 position = transformPart.position;

            createdCanvasCoins.transform.SetParent(transformPart);
            createdCanvasCoins.transform.position = new Vector3(position.x, position.y + 2f, position.z);
        }
        
        public Tweener CycleText(CanvasGroup canvas, BuildingPartSettings settings)
        {
            //DOTween.Sequence().Join(0,);
            _moveAnimation = canvas.transform.DOMoveY(canvas.transform.position.y + settings.OffsetPosition, settings.Duration).SetEase(Ease.OutQuad);
            return _fadeOutAnimation = canvas.DOFade(0f, settings.Duration).SetEase(Ease.Linear);
        }
        
        public override void Restart()
        {
            StopAnimation();
        }

        public void StopAnimation()
        {
            _moveAnimation.Kill();
            _fadeOutAnimation.Kill();
        }
    }
}