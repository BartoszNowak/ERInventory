using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InvSlots : MonoBehaviour
{
    private const int ColumnCount = 5;

    [SerializeField]
    private GameObject _slotsContainer = null;
    [SerializeField]
    private GameObject _subCategoryContainer = null;
    [SerializeField]
    private GameObject _dividerPrefab = null;
    [SerializeField]
    private GameObject _slotPrefab = null;
    [SerializeField]
    private int _slots = 25;

    private InventoryModel _inventory;

    private List<Button> _buttons = new List<Button>();

    private List<Selectable> _selectables = new List<Selectable>();

    void Start()
    {
        if (_slotsContainer == null || _slotPrefab == null) return;

        _inventory = GetComponent<InventoryModel>();
        int totalSlots = 0;

        var items = _inventory.Items;

        var subCategories = Enum.GetValues(typeof(ItemSubcategory)).Cast<ItemSubcategory>().ToArray();
        for (int a = 0; a < subCategories.Length; a++)
		{
            var subCategoryContainer = Instantiate(_subCategoryContainer, _slotsContainer.transform);
            int categoryCount = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].Subcategory == subCategories[a])
				{
                    categoryCount++;
                    var s = Instantiate(_slotPrefab, subCategoryContainer.transform);
                    totalSlots++;

                    var currentItem = items[i];
                    s.GetComponent<Slot>().Setup(currentItem.Icon, currentItem.Amount);
                }
                
            }
            var rows = Mathf.CeilToInt((float)categoryCount / 5);
            Debug.Log($"categoryCount: {categoryCount}, rows: {rows}");

            for (int i = categoryCount; i < rows * 10; i++)
            {
                Instantiate(_slotPrefab, subCategoryContainer.transform);
                totalSlots++;
            }
            if(categoryCount > 0 && totalSlots < 25)
			{
                Instantiate(_dividerPrefab, _slotsContainer.transform);
            }
        }

        
        if (totalSlots < 25)
		{
            var subCategoryContainer = Instantiate(_subCategoryContainer, _slotsContainer.transform);
            for (int i = _selectables.Count; i < 25; i++)
            {
                Instantiate(_slotPrefab, subCategoryContainer.transform);
            }
        }
	}
}
