using UnityEngine;

namespace Ingame
{
    public class MainImageManager : MonoBehaviour
    {
        private void Awake()
        {
            DI.di.SetMainImageTransform(GetComponent<RectTransform>());
        }
    }
}