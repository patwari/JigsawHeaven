using UnityEngine;
using UnityEngine.UI;
using Events;
using System.Collections.Generic;
using TMPro;

namespace Ingame
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private GameObject tilesContainer;
        [SerializeField] private TextMeshProUGUI counter;
        private List<MaskedTile> tiles;

        private int readyTiles = 0;

        private void Awake()
        {
            tiles = new List<MaskedTile>();
            for (int i = 0; i < tilesContainer.transform.childCount; i++)
            {
                MaskedTile t = tilesContainer.transform.GetChild(i).GetComponent<MaskedTile>();
                if (t != null) tiles.Add(t);
            }

            // Check :: if all tiles are ready 
            SubscribeEvents();
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].isReady)
                    readyTiles++;
            }

            if (readyTiles == tiles.Count)
                OnAllTilesReady();
        }

        private void SubscribeEvents()
        {
            IngameEventsModel.TILE_READY += OnTileReady;
            IngameEventsModel.TILE_DRAG_COMPLETE += OnTileDragComplete;
        }

        private void UnsubscribeEvents()
        {
            IngameEventsModel.TILE_READY -= OnTileReady;
            IngameEventsModel.TILE_DRAG_COMPLETE -= OnTileDragComplete;
        }

        private void OnDestroy() => UnsubscribeEvents();

        private void OnTileReady(int x, int y)
        {
            readyTiles++;
            if (readyTiles == tiles.Count)
                OnAllTilesReady();
        }

        private void OnAllTilesReady()
        {
            Debug.Log("patt :: all tiles are ready");
            IngameEventsModel.TILE_READY -= OnTileReady;
            ShuffleAllTilesPositions();
            IngameEventsModel.GRID_MANAGER_READY?.Invoke();

            // bring down the tiles container to the bottom
            tilesContainer.transform.localPosition = tilesContainer.transform.localPosition + new Vector3(0, -1000, 0);
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
            int count = GetCorrectTileCount();
            counter.text = $"Completed = {count}/{tiles.Count}";
            if (count == tiles.Count)
            {
                Debug.Log("patt :: win");
                IngameEventsModel.BEGIN_GAME_OVER?.Invoke();
            }
        }

        private int GetCorrectTileCount()
        {
            int count = 0;
            for (int i = 0; i < tiles.Count; i++)
                if (tiles[i].IsCorrect)
                    count++;
            return count;
        }
    }
}