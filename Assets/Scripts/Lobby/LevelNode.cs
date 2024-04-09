using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Events;

namespace Lobby
{
    public class LevelNode : MonoBehaviour
    {
        private Button _button;
        private int idx;
        private TextMeshProUGUI _label;

        private void Awake()
        {
            idx = transform.GetSiblingIndex() + 1;
            _label = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _label.text = idx.ToString();

            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Debug.Log($"LevelNode: {idx} clicked");
            LobbyEventsModel.LOBBY_NODE_CLICKED?.Invoke(idx);
        }
    }
}