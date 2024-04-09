using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class SoundableButton : MonoBehaviour
    {
        [SerializeField] private string soundId;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            if (_button == null) Debug.LogError($"SoundableButton: button is null :: {this.name}");
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (string.IsNullOrEmpty(soundId)) DI.di.soundManager.PlayDefaultButtonClick();
            else DI.di.soundManager.PlaySfx(soundId);
        }
    }
}
