using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCategoryUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _categoryName;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Image _previousIcon;
    [SerializeField]
    private Image _nextIcon;
    [SerializeField]
    private ItemCategoryState _defaultState;

    private ItemCategoryState _currentState;

    void Start()
    {
        _currentState = _defaultState;
        UpdateIcon();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
		{
            _currentState = _currentState.Previous;
		}
        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentState = _currentState.Next;
        }
        UpdateIcon();
    }

    private void UpdateIcon()
	{
        _categoryName.text = _currentState.CategoryName;
        _icon.sprite = _currentState.CategoryIcon;
        _previousIcon.sprite = _currentState.Previous.CategoryIcon;
        _nextIcon.sprite = _currentState.Next.CategoryIcon;
    }
}
