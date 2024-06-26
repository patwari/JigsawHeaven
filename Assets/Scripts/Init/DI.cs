using System;
using UnityEngine.Events;
using Sound;
using UnityEngine;
using Saver;
using Ingame;

/**
This is the central DI, which contains references to all objects, which are useful for the game
*/
public class DI
{
    public static DI di { get; } = new DI();

    public SoundManager soundManager { get; private set; } = null;
    public RectTransform mainImageTransform { get; private set; } = null;
    public ProgressSaver saver { get; private set; } = null;
    public TileManager tileManager { get; private set; } = null;

    private LevelImgSO _lvlImgSo = null;
    public LevelImgSO levelImgSO
    {
        get
        {
            if (_lvlImgSo == null) _lvlImgSo = Resources.Load<LevelImgSO>("SO/LevelImgSO");
            Debug.Log($"DI :: LevelImgSO: is = {_lvlImgSo != null}");
            return _lvlImgSo;
        }
    }

    public void SetSoundManager(SoundManager soundManager) => this.soundManager = soundManager;
    public void SetMainImageTransform(RectTransform mainImageTransform) => this.mainImageTransform = mainImageTransform;
    public void SetProgressSaver(ProgressSaver saver) => this.saver = saver;
    public void SetTileManager(TileManager tileManager) => this.tileManager = tileManager;
}