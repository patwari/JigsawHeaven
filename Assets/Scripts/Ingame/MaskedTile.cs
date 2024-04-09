using UnityEngine;
using UnityEngine.UI;
using Events;
using UnityEngine.EventSystems;
using GameConstants;
using Saver;

namespace Ingame
{
    public class MaskedTile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private Image mask;
        [SerializeField] private Image image;

        // top left is 0-0. top right is 0-max
        public int x = -1;
        public int y = -1;

        private RectTransform _rect = null;
        public bool isReady { get; private set; } = false;
        private TileState _state = TileState.INIT;

        public bool IsCorrect => _state == TileState.CORRECT;

        // variables related to dragging
        private Vector2 _offset = Vector2.zero;
        private Vector2 _startPos = Vector2.zero;
        private Vector2 _origPos = Vector2.zero;
        private int _origSI = -1;

        private RectTransform _mainImageTransform => DI.di.mainImageTransform;

        private void Awake()
        {
            image.rectTransform.sizeDelta = new Vector2(1000, 1000);
            image.rectTransform.sizeDelta = new Vector2(_mainImageTransform.rect.width, _mainImageTransform.rect.height); ;
            image.transform.position = _mainImageTransform.position;

            _rect = GetComponent<RectTransform>();

            // save the original position, so that we know if the tile is in correct position or not
            _origPos = _rect.position;
            // SubscribeEvents();
        }

        public void MakeReady(SModel_MaskedTile prvData)
        {
            if (prvData != null)
            {
                _state = prvData.state;
                _rect.position = prvData.rectPosition;
                _origSI = prvData.origSI;
                _origPos = prvData.origPos;
                Debug.Log($"patt :: recovering tile");
            }
            else
            {
                _state = TileState.INCORRECT;
                _rect.position = _origPos;
                Debug.Log($"patt :: new tile");
            }
            isReady = true;
        }

        // private void SubscribeEvents() => IngameEventsModel.GRID_MANAGER_READY += OnGridManagerReady;
        // private void UnsubscribeEvents() => IngameEventsModel.GRID_MANAGER_READY -= OnGridManagerReady;
        // private void OnDestroy() => UnsubscribeEvents();

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_state != TileState.INCORRECT) return;
            _startPos = Camera.main.ScreenToWorldPoint(eventData.position);
            _offset = _rect.position.ToV2() - _startPos;
            _state = TileState.DRAGGING;
            _origSI = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_state == TileState.DRAGGING)
            {
                _state = TileState.INCORRECT;
                OnDragComplete();
            }
        }

        private void OnDragComplete()
        {
            _startPos = Vector2.zero;
            _offset = Vector2.zero;
            transform.SetSiblingIndex(_origSI);
            IngameEventsModel.TILE_DRAG_COMPLETE?.Invoke(x, y);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_state != TileState.DRAGGING) return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(eventData.position).ToV2();
            Vector2 newPos = mousePos + _offset;

            if (Vector2.Distance(newPos, _origPos) <= IngameConstants.SNAP_THRESHOLD)
            {
                _rect.position = _origPos;
                if (IngameConstants.STOP_DRAG_AFTER_SNAP)
                {
                    _state = TileState.CORRECT;
                    OnDragComplete();
                }
            }
            else _rect.position = newPos;
        }

        public SModel_MaskedTile GetSaveData()
        {
            SModel_MaskedTile model = new SModel_MaskedTile();
            model.IsCorrect = IsCorrect;
            model.origPos = _origPos;
            model.origSI = _origSI;
            model.rectPosition = _rect.position;
            model.state = _state;
            return model;
        }
    }
}