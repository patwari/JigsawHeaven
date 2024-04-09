using UnityEngine;
using UnityEngine.UI;
using Events;
using System.Collections.Generic;
using TMPro;
using Saver;

namespace Ingame
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private GameObject tilesContainer;
        [SerializeField] private TextMeshProUGUI counter;
        [SerializeField] private Button quitButton;
        [SerializeField] private GameObject youWonPanel;

        private List<MaskedTile> tiles;
        private IngameState _state = IngameState.INIT;

        private int readyTiles = 0;

        private void Awake()
        {
            DI.di.SetTileManager(this);
            tiles = new List<MaskedTile>();
            for (int i = 0; i < tilesContainer.transform.childCount; i++)
            {
                MaskedTile t = tilesContainer.transform.GetChild(i).GetComponent<MaskedTile>();
                if (t != null) tiles.Add(t);
            }
            SubscribeEvents();
            youWonPanel.SetActive(false);
            quitButton.interactable = false;
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void Start()
        {
            if (DI.di.saver.model != null)
            {
                _state = IngameState.RESTORE;
                for (int i = 0; i < tiles.Count; i++)
                    tiles[i].MakeReady(DI.di.saver.model.tiles[i]);
                OnAllTilesReady(true);
            }
            else
            {
                for (int i = 0; i < tiles.Count; i++)
                    tiles[i].MakeReady(null);
                OnAllTilesReady(false);
            }
        }

        private void SubscribeEvents() => IngameEventsModel.TILE_DRAG_COMPLETE += OnTileDragComplete;

        private void UnsubscribeEvents() => IngameEventsModel.TILE_DRAG_COMPLETE -= OnTileDragComplete;

        private void OnDestroy()
        {
            UnsubscribeEvents();
            DI.di.SetTileManager(null);
            SaveProgress();
        }

        private void OnApplicationFocus(bool focusStatus)
        {
            // save only on focus lost
            if (!focusStatus) SaveProgress();
        }

        private void OnAllTilesReady(bool restoring)
        {
            Debug.Log("OnAllTilesReady :: all tiles are ready");
            _state = IngameState.PLAY;
            quitButton.interactable = true;

            if (restoring)
            {
                OnTileDragComplete(0, 0);
            }
            else
            {
                ShuffleAllTilesPositions();
                tilesContainer.transform.localPosition = tilesContainer.transform.localPosition + new Vector3(0, -1000, 0);
                counter.text = $"Completed = 0/{tiles.Count}";
            }
            EventsModel.FORCE_HIDE_LOADER?.Invoke();
        }

        // shuffle using Fisher-Yates shuffle algorithm
        private void ShuffleAllTilesPositions()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                Vector3 tempPosition = tiles[i].transform.position;

                // Find a random index within the array
                int randomIndex = Random.Range(i, tiles.Count);

                // Swap positions with the object at the random index
                tiles[i].transform.position = tiles[randomIndex].transform.position;
                tiles[randomIndex].transform.position = tempPosition;
            }
        }

        private void OnTileDragComplete(int x, int y)
        {
            Debug.Log($"OnTileDragComplete :: tile drag complete :: {x}, {y}");
            int count = GetCorrectTileCount();
            counter.text = $"Completed = {count}/{tiles.Count}";
            if (count == tiles.Count)
                OnWin();
        }

        private int GetCorrectTileCount()
        {
            int count = 0;
            for (int i = 0; i < tiles.Count; i++)
                if (tiles[i].IsCorrect)
                    count++;
            return count;
        }

        public void SaveProgress()
        {
            Debug.Log("SaveProgress :: save progress ::  state = " + _state);

            if (_state == IngameState.WIN || _state == IngameState.LOSE)
            {
                DI.di.saver.model = null;
                DI.di.saver.DeleteFile();
                PlayerPrefs.DeleteKey("level_to_play");
                return;
            }

            if (_state == IngameState.PLAY)
            {
                SModel_TileManager model = new SModel_TileManager();
                model.tiles = new List<SModel_MaskedTile>();
                for (int i = 0; i < tiles.Count; i++)
                    model.tiles.Add(tiles[i].GetSaveData());

                DI.di.saver.model = model;
                DI.di.saver.Save();
            }
        }

        private void OnWin()
        {
            _state = IngameState.WIN;
            quitButton.gameObject.SetActive(false);
            // IngameEventsModel.BEGIN_GAME_OVER?.Invoke();
            Invoke("ShowGameOverAfterDelay", 1f);
        }

        private void ShowGameOverAfterDelay() => youWonPanel.SetActive(true);

        private void OnQuitClicked()
        {
            _state = IngameState.LOSE;
            DI.di.saver.model = null;
            DI.di.saver.DeleteFile();
            PlayerPrefs.DeleteKey("level_to_play");
            EventsModel.LOAD_SCENE?.Invoke("Lobby", false, "Going to Lobby...");
        }
    }
}