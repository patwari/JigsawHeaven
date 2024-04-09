using UnityEngine;
using UnityEngine.UI;
using Events;
using UnityEngine.EventSystems;
using GameConstants;
using System;

namespace Saver
{
    [Serializable]
    public class SModel_MaskedTile
    {
        public bool IsCorrect;
        public Vector2 origPos;
        public int origSI;
        public Vector2 rectPosition;
        public TileState state;
    }
}