using UnityEngine;

public class ProgressRestoreSceneManager : MonoBehaviour
{
    private void Start()
    {
        // check if model exists
        if (DI.di.saver.model == null)
        {
            Events.EventsModel.LOAD_SCENE?.Invoke(GameConstants.Scenes.LOBBY, false, "Loading Lobby...");
        }
        else
        {
            int levelToPlay = PlayerPrefs.GetInt("level_to_play");
            Debug.Log($"ProgressRestoreSceneManager :: levelToPlay: {levelToPlay}");
            Events.EventsModel.LOAD_SCENE?.Invoke(GameConstants.Scenes.GAMEPLAY, false, $"Loading Game {levelToPlay}...");
        }
    }
}