using CityBuilder.Building;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CityBuilder
{
    public class ViewCoins : RestartEntity
    {
        [SerializeField] private TMP_Text _price;

        private Tweener _moveAnimation;
        private Tweener _fadeOutAnimation;
        
        public void UpdatePrice(int price)
        {
            _price.text = price.ToString();
        }
        
        public void SetPosition(Transform transformPart, Transform createdCanvasCoins)
        {
            Vector3 position = transformPart.position;

            createdCanvasCoins.transform.parent = transformPart;
            createdCanvasCoins.transform.position = new Vector3(position.x, position.y + 2f, position.z);
        }
        
        public Tweener CycleText(CanvasGroup canvas, BuildingPartSettings settings)
        {
            _moveAnimation = canvas.transform.DOMoveY(canvas.transform.position.y + settings.OffsetPosition, settings.Duration).SetEase(Ease.OutQuad);
            return _fadeOutAnimation = canvas.DOFade(0f, settings.Duration).SetEase(Ease.Linear);
        }
        
        public override void Restart()
        {
            _moveAnimation.Kill();
            _fadeOutAnimation.Kill();
        }
    }
}