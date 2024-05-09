using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CityBuilder.UI
{
    public class ClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _default;
        [SerializeField] private Sprite _pressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            _image.sprite = _pressed;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _image.sprite = _default;
        }
    }
}