using UnityEngine.Events;

namespace Events
{
    public static class LobbyEventsModel
    {
        public static UnityAction<int> LOBBY_NODE_CLICKED;  // idx. 1-based
    }
}