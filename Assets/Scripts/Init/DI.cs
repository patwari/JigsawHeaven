using System;
using UnityEngine.Events;
using Sound;

/**
This is the central DI, which contains references to all objects, which are useful for the game.clea
*/
public class DI
{
    public static DI di { get; } = new DI();

    public SoundManager soundManager { get; private set; } = null;

    public void SetSoundManager(SoundManager soundManager) => this.soundManager = soundManager;
}