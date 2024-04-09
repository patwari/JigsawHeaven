using UnityEngine;
using Events;

namespace Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        private void Awake()
        {
            LobbyEventsModel.LOBBY_NODE_CLICKED += OnNodeClicked;
        }

        private void OnNodeClicked(int idx)
        {
            Debug.Log($"OnNodeClicked :: OnNodeClicked :: {idx}");
            PlayerPrefs.SetInt("level_to_play", idx);
            EventsModel.LOAD_SCENE?.Invoke(GameConstants.Scenes.GAMEPLAY, false, $"Loading Game {idx}...");
        }

        private void OnDestroy() => LobbyEventsModel.LOBBY_NODE_CLICKED -= OnNodeClicked;
    }
}