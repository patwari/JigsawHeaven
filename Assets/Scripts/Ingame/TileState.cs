namespace GameConstants
{
    public enum TileState
    {
        INIT,       // not ready yet
        INCORRECT,  // placed at incorrect position
        DRAGGING,   // currently being dragged
        CORRECT     // placed at correct position
    }
}