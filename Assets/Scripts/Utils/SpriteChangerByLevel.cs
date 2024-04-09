using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class SpriteChangerByLevel : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.sprite = DI.di.levelImgSO.GetSpriteForCurrLevel();
        }
    }
}