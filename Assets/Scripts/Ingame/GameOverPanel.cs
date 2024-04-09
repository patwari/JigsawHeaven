using UnityEngine;
using UnityEngine.UI;
using Events;
using System.Collections.Generic;
using TMPro;
using Saver;

namespace Ingame
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private Button lobbyButton;

        private void Awake() => lobbyButton.onClick.AddListener(OnLobbyClicked);
        private void OnLobbyClicked() => EventsModel.LOAD_SCENE?.Invoke("Lobby", false, "Going to Lobby...");
    }
}