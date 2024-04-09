using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelImgSO", menuName = "SO/LevelImgSO")]
public class LevelImgSO : ScriptableObject
{
    public List<Sprite> bg = new List<Sprite>();

    public Sprite GetSpriteForCurrLevel()
    {
        int lvl = PlayerPrefs.GetInt("level_to_play") - 1;
        return bg[lvl];
    }
}