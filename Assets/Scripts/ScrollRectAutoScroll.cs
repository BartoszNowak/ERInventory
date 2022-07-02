using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectAutoScroll : MonoBehaviour
{
    [SerializeField]
    private int _rows = 5;
    [SerializeField]
    private int _coulums = 5;

    private List<Selectable> _selectables = new List<Selectable>();
    private ScrollRect _scrollRect;
    private Vector2 _nextScrollPosition = Vector2.up;
    private int _totalRows;
    private float _scrollMoveFraction;
    private int _lastSelectedSlotIndex;

    void OnEnable()
    {
        if (_scrollRect)
        {
            _scrollRect.content.GetComponentsInChildren(_selectables);
        }
    }

    void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    void Start()
    {
        if (_scrollRect)
        {
            _scrollRect.content.GetComponentsInChildren(_selectables);
            if(_selectables.Count > 0)
			{
                EventSystem.current.firstSelectedGameObject = _selectables[0].gameObject;
			}
        }
        Setup();
    }

    void Update()
    {
        TriggerScroll();
    }

    private void TriggerScroll()
	{
        Selectable selectedElement = EventSystem.current.currentSelectedGameObject ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : null;
        int selectedIndex = 0;
        if (selectedElement)
        {
            selectedIndex = _selectables.IndexOf(selectedElement);
        }
        if (selectedIndex != _lastSelectedSlotIndex)
        {
            var lastRow = Mathf.CeilToInt(_lastSelectedSlotIndex / 5);
            var selectedRow = Mathf.CeilToInt(selectedIndex / 5);

            if (selectedRow == lastRow) return;

            //Debug.Log($"Changed from [slot {_lastSelectedSlotIndex} row {lastRow}] to [slot {selectedIndex} row {selectedRow}] position {_scrollRect.verticalNormalizedPosition} | visible {FirstVisibleRowIndex}-{LastVisibleRowIndex}");

            if (selectedRow > lastRow && selectedRow > LastVisibleRowIndex - 1)
			{
                var newValue = _nextScrollPosition.y - _scrollMoveFraction;
                _nextScrollPosition = new Vector2(0, Mathf.Clamp01(newValue));
            } 
            else if (selectedRow < lastRow && selectedRow < FirstVisibleRowIndex + 1)
            {
                var newValue = _nextScrollPosition.y + _scrollMoveFraction;
				_nextScrollPosition = new Vector2(0, Mathf.Clamp01(newValue));
			}
            //Debug.Log($"Changed from [slot {lastSelected} row {lastRow}] to [slot {selectedIndex} row {selectedRow}] position {m_ScrollRect.verticalNormalizedPosition} | visible {FirstVisibleRowIndex}");

			StartCoroutine(ScrollTo(_scrollRect.normalizedPosition, _nextScrollPosition, 0.1f));
			_lastSelectedSlotIndex = selectedIndex;
        }
    }

	private IEnumerator ScrollTo(Vector2 startPos, Vector2 endPos, float time)
	{
		float i = 0.0f;
		float rate = 1.0f / time;
		while (i < 1.0)
		{
			yield return new WaitForEndOfFrame();
			i += (Time.deltaTime * rate);
            _scrollRect.normalizedPosition = Vector2.Lerp(startPos, endPos, i);
		}
	}

	private int FirstVisibleRowIndex => (int)((1f - _nextScrollPosition.y) / _scrollMoveFraction);
    private int LastVisibleRowIndex => FirstVisibleRowIndex + _rows - 1;

    private void Setup()
	{
        var childCount = _selectables.Count;
        var hiddenSlots = Mathf.Max(childCount - _rows * _coulums, 0);
        _totalRows = Mathf.CeilToInt(hiddenSlots / 5);
        _scrollMoveFraction = 1f / _totalRows;
        //Debug.Log($"{childCount} -> {hiddenSlots} -> {hiddenSlots / 5} -> {_totalRows} -> {_scrollMoveFraction}");
    }
}