using UnityEngine;
using UnityEngine.UI;
using Events;
using System.Collections.Generic;
using TMPro;
using System;

namespace Saver
{
    [Serializable]
    public class SModel_TileManager
    {
        public List<SModel_MaskedTile> tiles;
        public int level = -1;   // 1-index 
    }
}