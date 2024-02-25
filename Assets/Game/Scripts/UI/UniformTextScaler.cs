using TMPro;
using UnityEngine;

namespace CityBuilder.UI
{
    public class UniformTextScaler : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _texts; 

        void Start()
        {
            float minFontSize = FindMinFontSize();
            Debug.Log("мин размер" + minFontSize);

            SetTextFontSize(minFontSize);
        }

        private float FindMinFontSize()
        {
            float minFontSize = int.MaxValue;

            foreach (TMP_Text textElement in _texts)
            {
                if (textElement != null && textElement.fontSize < minFontSize)
                {
                    minFontSize = textElement.fontSize;
                }
            }

            return minFontSize;
        }

        private void SetTextFontSize(float fontSize)
        {
            foreach (TMP_Text textElement in _texts)
            {
                textElement.fontSizeMin = fontSize;
                textElement.fontSizeMax = fontSize;
                //textElement.fontSize = fontSize;
                Debug.Log("+");
            }
        }
    }
}