using UnityEngine.Events;

namespace Events
{
    public static partial class EventsModel
    {
        public static UnityAction<string, bool, string> LOAD_SCENE;
        public static UnityAction FORCE_HIDE_LOADER;
    }
}