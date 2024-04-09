using UnityEngine.Events;

namespace Events
{
    public static class IngameEventsModel
    {
        public static UnityAction<int, int> TILE_READY;
        public static UnityAction GRID_MANAGER_READY;
        public static UnityAction<int, int> TILE_DRAG_COMPLETE;
        public static UnityAction BEGIN_GAME_OVER;
    }
}